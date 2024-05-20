using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCleanWithMediatr.Domain.Entities
{
    public  class Category
    {
        [Key]
        public int CategoryId { get; set; }

        // Category name
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Navigation property - One category can have many orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
