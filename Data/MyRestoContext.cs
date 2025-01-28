using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myresto.Models;

namespace myresto.Data
{
    public class MyRestoContext : IdentityDbContext<ApplicationUser>
    {
        public MyRestoContext(DbContextOptions<MyRestoContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
     
        public DbSet<Order> Orders { get; set; }
       
        public DbSet<Payment> Payments { get; set; }
   
    }
}
