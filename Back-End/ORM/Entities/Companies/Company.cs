using Dietbox.ECommerce.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Entities.Companies
{

    [Table("Companies", Schema = "dbo")]
    public class Company : IIdentity, ICreatedDate
    {

        /// <summary>
        /// Identificador da empresa.
        /// </summary>
        [Key]
        [Required]
        [Column("ID")]
        public int ID { get; set; }

        /// <summary>
        /// Nome da empresa.
        /// </summary>
        [Required]
        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// CNPJ da empresa.
        /// </summary>
        [Required]
        [Column("CNPJ")]
        [MaxLength(14)]
        public string CNPJ { get; set; }

        /// <summary>
        /// E-mail de acesso da empresa.
        /// </summary>
        [Required]
        [Column("Email")]
        [MaxLength(80)]
        public string Email { get; set; }


        /// <summary>
        /// Senha de acesso da empresa.
        /// </summary>
        [Required]
        [Column("Password")]
        [MaxLength(64)]
        public string Password { get; set; }

        /// <summary>
        /// Data/Hora de cadastro da empresa no sistema.
        /// </summary>
        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

    }
}
