using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myresto.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserName { get; set; }    

        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
       
    }
}
