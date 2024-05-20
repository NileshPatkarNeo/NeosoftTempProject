using Microsoft.EntityFrameworkCore;
using System;
using WebApiCleanWithMediatr.Domain.Entities;

namespace WebApiCleanWithMediatr.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
