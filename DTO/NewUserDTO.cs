using System.ComponentModel.DataAnnotations;

namespace myresto.DTO
{
    public class NewUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
