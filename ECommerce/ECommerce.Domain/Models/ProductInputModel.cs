using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Models
{
    public class ProductInputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters and max 100.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters and max 100.", MinimumLength = 3)]
        public string BarCode { get; set; }
    }
}
