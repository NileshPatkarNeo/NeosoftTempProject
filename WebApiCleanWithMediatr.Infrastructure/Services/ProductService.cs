using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Application.Interfaces;


//using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Domain.DTO;
using WebApiCleanWithMediatr.Domain.Entities;

namespace WebApiCleanWithMediatr.Infrastructure.Services
{


    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                // Set other properties as needed
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                // Set other properties as needed
            };
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                // Set other properties as needed
            };
        }

    }

}
