using Dietbox.ECommerce.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Entities.Products
{

    [Table("Products", Schema = "company")]
    public class Product : IIdentity, ICreatedDate
    {
        /// <summary>
        /// Identificador do produto.
        /// </summary>
        [Key]
        [Required]
        [Column("ID")]
        public int ID { get; set; }

        /// <summary>
        /// Identificador da empresa que o produto está vinculado.
        /// </summary>
        [Required]
        [Column("CompanyID")]
        public int CompanyID { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Descrição do produto.
        /// </summary>
        [Column("Description")]
        [MaxLength(400)]
        public string? Description { get; set; }

        /// <summary>
        /// Marca do produto.
        /// </summary>
        [Column("Brand")]
        [MaxLength(50)]
        public string? Brand { get; set; }

        /// <summary>
        /// Valor do produto.
        /// </summary>
        [Required]
        [Column("Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Código interno do produto.
        /// </summary>
        [Column("Code")]
        [MaxLength(50)]
        public string? Code { get; set; }

        /// <summary>
        /// Estoque disponível.
        /// </summary>
        [Required]
        [Column("Stock")]
        public int Stock { get; set; }

        /// <summary>
        /// Flag indicando se o produto está ativo.
        /// </summary>
        [Required]
        [Column("Active")]
        public bool Active { get; set; }

        /// <summary>
        /// Data/Hora de cadastro do produto no sistema.
        /// </summary>
        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

    }
}
