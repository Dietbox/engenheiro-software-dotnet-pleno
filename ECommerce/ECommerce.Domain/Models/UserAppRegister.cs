using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Models
{
    public class UserAppRegister : UserLogin
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters and max 100.", MinimumLength = 6)]
        public string FullName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }
    }
}
