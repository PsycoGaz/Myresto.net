﻿using System.ComponentModel.DataAnnotations;

namespace myresto.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }


}
