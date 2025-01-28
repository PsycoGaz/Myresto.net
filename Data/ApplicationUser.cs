namespace myresto.Data
{
    using Microsoft.AspNetCore.Identity;
    using myresto.Models;

    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
