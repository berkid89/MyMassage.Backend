using System.ComponentModel.DataAnnotations;

namespace MyMassage.Backend.Models
{
    public class Subscription : BusinessEntityBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
