using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Application.Interfaces;
using WebApiCleanWithMediatr.Domain.DTO;
using WebApiCleanWithMediatr.Domain.Entities;

namespace WebApiCleanWithMediatr.Infrastructure.Services
{
    public class CategoryService : ICategorysService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<CategoryDto> CreateCategoryAsync(CreateCategorysCommand command)
        {
            // Create a new Category entity and set its properties
            var category = new Category
            {
                Name = command.Name,
                // Set other properties as needed
            };

            // Add the new Category entity to the DbContext and save changes
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            // Return a new CategoryDto with the created category's data
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                // Set other properties as needed
            };
        }

        // Method to get a category by its ID
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            // Find the Category entity with the specified ID
            var category = await _dbContext.Categories.FindAsync(id);

            // If no category is found, return null
            if (category == null)
                return null;

            // Return a new CategoryDto with the found category's data
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                // Set other properties as needed
            };

        }
    }
}
