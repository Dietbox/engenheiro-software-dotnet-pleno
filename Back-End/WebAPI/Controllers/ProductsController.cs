using Dietbox.ECommerce.Core.Commands.Products;
using Dietbox.ECommerce.Core.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Dietbox.ECommerce.WebAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("products")]
    [ApiExplorerSettings(GroupName = "Produtos")]
    public class ProductsController : BaseController
    {
        private readonly IMemoryCache _cache;
        private readonly IProductsHandler _handler;
        private readonly IProductsQueries _queries;

        public ProductsController(IMemoryCache cache, IProductsHandler handler, IProductsQueries queries)
        {
            _cache = cache;
            _handler = handler;
            _queries = queries;
        }

        /// <summary>
        /// [ EndPoint ] Buscar lista de produtos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheEntry = _cache.GetOrCreate("ProductsListCache", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                entry.SetPriority(CacheItemPriority.High);
                var result = _queries.Get().GetAwaiter().GetResult();
                return result;
            });

            return Ok(cacheEntry);
        }


        /// <summary>
        /// [ EndPoint ] Buscar produto.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var cacheEntry = _cache.GetOrCreate($"Product{id}Cache", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                entry.SetPriority(CacheItemPriority.High);
                var result = _queries.Get(id).GetAwaiter().GetResult();
                return result;
            });

            return Ok(cacheEntry);
        }

        /// <summary>
        /// [ EndPoint ] Criar produto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _handler.Create(command);
            return Ok(result, $"O produto '{command.Name}' foi criado com êxito.");
        }

        /// <summary>
        /// [ EndPoint ] Atualizar produto (não implementado).
        /// </summary>
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, object command)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// [ EndPoint ] Excluir produto.
        /// </summary>
        [HttpDelete("{id:int}")]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }


        [HttpPost("{id:int}/buy")]
        public async Task<IActionResult> Buy([FromRoute] int id, BuyProductCommand command)
        {
            command.ID = id;
            await _handler.Buy(command);
            _cache.Remove($"Product{id}Cache");
            return Ok("Compra efetuada com êxito.");
        }

    }
}
