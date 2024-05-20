using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCleanWithMediatr.Application.Commands;
using WebApiCleanWithMediatr.Domain.DTO;

namespace WebApiCleanWithMediatr.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderCommand command);
        Task<OrderDto> GetOrderByIdAsync(int id);
    }
}
