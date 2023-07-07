using ECommerce.Domain.Auth;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Cria um produto. Somente uma empresa pode criar um produto.
        /// </summary>
        /// <returns>Sem Retorno</returns>
        /// <response code="204">Sem Retorno</response>
        /// <response code="400">Dados do produto não seguem os requisitos mínimos</response>
        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_COMPANY)]
        public async Task<IActionResult> Create([FromBody] ProductInputModel product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product, User.Identity.Name);

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Lista os produtos. A empresa verá somente seus próprios produtos.
        /// </summary>
        /// <returns>Sem Retorno</returns>
        /// <response code="200">Lista de todos os produtos da empresa</response>
        [HttpGet]
        [Route("listcompanyproducts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_COMPANY)]
        public async Task<IActionResult> ListCompanyProducts()
        {
            var products = await _productService.ListProductsByCompany(User.Identity.Name);

            return Ok(products);
        }

        /// <summary>
        /// Lista todos os produtos. Somente os usuários comuns e os admin podem ver todos os produtos.
        /// </summary>
        /// <returns>Sem Retorno</returns>
        /// <response code="200">Lista de todos os produtos</response>
        [HttpGet]
        [Route("listallproducts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.ROLE_ADMIN},{Roles.ROLE_API}")]
        public IActionResult ListAllProducts()
        {
            var products = _productService.ListAllProducts();

            return Ok(products);
        }
    }
}
