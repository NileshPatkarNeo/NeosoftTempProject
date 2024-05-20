using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Domain.DTO;

namespace WebApiCleanWithMediatr.Application.Interfaces
{
    public interface ICategorysService
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategorysCommand command);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
    }
}
