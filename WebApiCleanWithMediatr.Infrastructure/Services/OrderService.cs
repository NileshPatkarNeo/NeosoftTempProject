using Microsoft.EntityFrameworkCore;
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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderCommand command)
        {
            var order = new Order
            {
                OrderDate = command.OrderDate,
             
                CategoryId = command.CategoryId
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var category = await _dbContext.Categories.FindAsync(order.CategoryId);

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
              
                CategoryId = order.CategoryId,
                CategoryName = category?.Name
            };
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Category)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                
                CategoryId = order.CategoryId,
                CategoryName = order.Category.Name
            };
        }
    }


}
