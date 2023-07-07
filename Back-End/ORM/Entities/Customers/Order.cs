using Dietbox.ECommerce.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Entities.Customers
{

    [Table("Orders", Schema = "customer")]
    public class Order : IIdentity, ICreatedDate
    {

        /// <summary>
        /// Identificador da ordem de compra.
        /// </summary>
        [Key]
        [Required]
        [Column("ID")]
        public int ID { get; set; }

        /// <summary>
        /// Identificador do usuário que realizou a compra.
        /// </summary>
        [Required]
        [Column("UserID")]
        public int UserID { get; set; }

        /// <summary>
        /// Identificador do produto comprado.
        /// </summary>
        [Required]
        [Column("ProductID")]
        public int ProductID { get; set; }

        /// <summary>
        /// Quantidade do produto comprado.
        /// </summary>
        [Required]
        [Column("Amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Valor unitário do produto.
        /// </summary>
        [Required]
        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Valor total do pedido.
        /// </summary>
        [Required]
        [Column("TotalPrice")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Data de criação da ordem de compra.
        /// </summary>
        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

    }
}
