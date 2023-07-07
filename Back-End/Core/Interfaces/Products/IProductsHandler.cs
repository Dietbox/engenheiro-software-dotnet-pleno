using Dietbox.ECommerce.Core.Commands.Products;
using Dietbox.ECommerce.Core.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces.Products
{
    public interface IProductsHandler
    {

        /// <summary>
        /// Criar produto.
        /// </summary>
        /// <param name="command">Comando de criação de produto.</param>
        /// <returns></returns>
        Task<ProductDTO> Create(CreateProductCommand command);


        /// <summary>
        /// Excluir produto.
        /// </summary>
        /// <param name="command">Comando de exclusão do produto.</param>
        /// <returns></returns>
        Task Delete(DeleteProductCommand command);


        /// <summary>
        /// Comprar produto.
        /// </summary>
        /// <param name="command">Comando de compra do produto.</param>
        /// <returns></returns>
        Task Buy(BuyProductCommand command);
    }
}
