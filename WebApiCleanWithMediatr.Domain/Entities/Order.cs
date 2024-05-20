using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCleanWithMediatr.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Order date
        [Required]
        public DateTime OrderDate { get; set; }

        // Foreign key to Category
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Navigation property to Category
        public Category Category { get; set; }
    }
}
