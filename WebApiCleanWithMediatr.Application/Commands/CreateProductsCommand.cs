using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Interfaces;
using WebApiCleanWithMediatr.Domain.DTO;
//using WebApiCleanWithMediatr.Infrastructure.Services;


namespace WebApiCleanWithMediatr.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Other properties...
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Perform validation, authorization, etc.
            var product = await _productService.CreateProductAsync(request);
            return product;
        }
    }
}
