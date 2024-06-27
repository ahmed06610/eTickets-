using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Services
{
    public class OrderService : IOrderService
    {
        private readonly appdbcontext _context;

        public OrderService(appdbcontext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders =await _context.Orders.Include(o=>o.OrderItems).ThenInclude(n=>n.Movie).Include(n=>n.User).ToListAsync();

            if (userRole != UserRoles.Admin)
            {
                orders=orders.Where(o=>o.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
           var order=new Order()
           {
               UserId = userId,
               Email=userEmailAddress,
           };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items) 
            {
                var orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
               await _context.OrderItems.AddAsync(orderitem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
