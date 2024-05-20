using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Interfaces;
using WebApiCleanWithMediatr.Application.Queries;
using WebApiCleanWithMediatr.Domain.DTO;

namespace WebApiCleanWithMediatr.Application.Queries
{
    public class GetOrderByIdQuery  : IRequest<OrderDto>
    {
        public int Id { get; set; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderService _orderService;

    public GetOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.Id);
        return order;
    }
}
   
}
