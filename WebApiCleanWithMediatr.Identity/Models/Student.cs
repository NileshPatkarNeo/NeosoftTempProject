using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCleanWithMediatr.Identity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string? AppUserId { get; set; }
        public virtual AppUser? AppUsers { get; set; }
    }
}
