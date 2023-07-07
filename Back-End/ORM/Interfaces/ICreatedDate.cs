using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Interfaces
{
    public interface ICreatedDate
    {
        [Required]
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
