using System.ComponentModel.DataAnnotations;

namespace MyMassage.Backend.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
