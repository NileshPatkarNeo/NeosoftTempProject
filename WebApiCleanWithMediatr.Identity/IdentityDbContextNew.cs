using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Identity.Models;

namespace WebApiCleanWithMediatr.Identity
{
    public class IdentityDbContextNew : IdentityDbContext<AppUser>
    {


        public IdentityDbContextNew(DbContextOptions<IdentityDbContextNew> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Student> Students { get; set; }
    }
}
