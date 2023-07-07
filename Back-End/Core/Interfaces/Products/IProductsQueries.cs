using Dietbox.ECommerce.Core.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Interfaces.Products
{
    public interface IProductsQueries
    {

        /// <summary>
        /// Obter listagem de produtos.
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo 'ProdutoDTO'.</returns>
        Task<List<ProductDTO>> Get();

        /// <summary>
        /// Obter produto.
        /// </summary>
        /// <param name="id">Identificador do produto</param>
        /// <returns>Retorna um objeto do tipo 'ProdutoDTO'.</returns>
        Task<ProductDTO> Get(int id);
        
    }
}
