using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCleanWithMediatr.Domain.DTO
{
    public class OrderDto
    {
        
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
      
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
        
    }
}
