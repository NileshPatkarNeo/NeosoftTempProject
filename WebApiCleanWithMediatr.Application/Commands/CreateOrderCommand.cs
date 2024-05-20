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
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CategoryId { get; set; }


        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
        {
            private readonly IOrderService _orderService;

            public CreateOrderCommandHandler(IOrderService orderService)
            {
                _orderService = orderService;
            }

            public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderService.CreateOrderAsync(request);
                return order;
            }
        }
    }

}
