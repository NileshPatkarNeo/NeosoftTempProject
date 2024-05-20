using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Interfaces;
using WebApiCleanWithMediatr.Domain.DTO;

namespace WebApiCleanWithMediatr.Application.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
        {
            private readonly ICategorysService _categoryService;

            public GetCategoryByIdQueryHandler(ICategorysService categoryService)
            {
                _categoryService = categoryService;
            }

            public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryService.GetCategoryByIdAsync(request.Id);
                return category;
            }
        }
    }
}
