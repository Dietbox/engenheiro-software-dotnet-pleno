using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Models
{
    public class CompanyAppRegister : UserLogin
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters and max 100.", MinimumLength = 6)]
        public string TradingName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters and max 100.", MinimumLength = 3)]
        public string TaxId { get; set; }
    }
}
