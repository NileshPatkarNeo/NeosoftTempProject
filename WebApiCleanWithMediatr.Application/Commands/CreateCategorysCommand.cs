using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Interfaces;
using WebApiCleanWithMediatr.Domain.DTO;

namespace WebApiCleanWithMediatr.Application.Commands
{
    public class CreateCategorysCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }



        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategorysCommand, CategoryDto>
        {
            private readonly ICategorysService _categoryService;

            public CreateCategoryCommandHandler(ICategorysService categoryService)
            {
                _categoryService = categoryService;
            }

            public async Task<CategoryDto> Handle(CreateCategorysCommand request, CancellationToken cancellationToken)
            {
                // Perform validation, authorization, etc.
                var category = await _categoryService.CreateCategoryAsync(request);
                return category;
            }
        }
    }
}
