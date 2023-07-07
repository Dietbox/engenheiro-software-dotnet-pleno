using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.Companies
{
    public record CompanyDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
