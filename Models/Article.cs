using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace myresto.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }


}
