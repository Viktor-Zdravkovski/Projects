using MainStreet.DataBase.Context;
using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Enums;
using MainStreet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MainStreet.DataBase.Implementations.EFImplementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MainStreetDbContext _dbContext;

        public OrderRepository(MainStreetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetIdByAsync(int id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _dbContext.Orders.Include(x => x.User)
                                          .Include(o => o.OrderItems)
                                            .ThenInclude(oi => oi.Product)
                                          .Include(e => e.OrderItems)
                                            .ThenInclude(z => z.SelectedModifiers)
                                            .ThenInclude(c => c.Modifier)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(n => n.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        {
            var activeStatuses = new[]
            {
                OrderStatus.Pending,
                OrderStatus.Confirmed,
                OrderStatus.Cooking,
                OrderStatus.Ready,
                OrderStatus.Dispatched
            };

            return await _dbContext.Orders
                                   .AsNoTracking()
                                   .Include(x => x.OrderItems)
                                    .ThenInclude(i => i.Product)
                                   .Where(q => activeStatuses.Contains(q.Status))
                                   .OrderByDescending(g => g.CreatedAt)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersByUserIdAsync(int userId, int count)
        {
            if (count <= 0) count = 5;

            return await _dbContext.Orders
                                   .AsNoTracking()
                                   .Include(c => c.User)
                                   .Include(o => o.OrderItems)
                                       .ThenInclude(oi => oi.Product)
                                   .Where(x => x.UserId == userId)
                                   .OrderByDescending(x => x.CreatedAt)
                                   .Take(count)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Order>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<Order>();

            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Where(x => x.User.FirstName.Contains(name)
                         || x.User.LastName.Contains(name)
                         || x.CustomerName.Contains(name))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCertainDateAsync(DateTime date)
        {
            DateTime startOfDay = date.Date;
            DateTime endOfDay = startOfDay.AddDays(1);

            return await _dbContext.Orders
                                   .AsNoTracking()
                                   .Include(x => x.User)
                                   .Include(w => w.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                   .Where(x => x.CreatedAt >= startOfDay && x.CreatedAt < endOfDay)
                                   .ToListAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            Order? existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingOrder != null)
            {
                _dbContext.Entry(existingOrder).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Order entity)
        {
            await _dbContext.Orders.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
