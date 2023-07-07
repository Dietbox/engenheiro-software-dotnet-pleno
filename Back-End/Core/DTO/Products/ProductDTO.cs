using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.Products
{
    public class ProductDTO
    {

        /// <summary>
        /// Identificador do produto.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Identificador da empresa que o produto está vinculado.
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do produto.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Marca do produto.
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// Valor do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Código interno do produto.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Estoque disponível.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Flag indicando se o produto está ativo.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Data/Hora de cadastro do produto no sistema.
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
}
