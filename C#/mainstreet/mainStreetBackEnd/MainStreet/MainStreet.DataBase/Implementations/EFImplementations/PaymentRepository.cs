using MainStreet.DataBase.Context;
using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MainStreet.DataBase.Implementations.EFImplementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MainStreetDbContext _dbContext;

        public PaymentRepository(MainStreetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _dbContext.Payments.ToListAsync();
        }

        public async Task<Payment?> GetIdByAsync(int id)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Payment entity)
        {
            Payment? existingPayment = await _dbContext.Payments.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingPayment != null)
            {
                _dbContext.Entry(existingPayment).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Payment entity)
        {
            await _dbContext.Payments.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Payment entity)
        {
            _dbContext.Payments.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalRevenueByDateAsync(DateTime dateTime)
        {
            var startDate = dateTime.Date;
            var endDate = startDate.AddDays(1);

            return await _dbContext.Payments
                .Where(p => p.PaidAt >= startDate && p.PaidAt < endDate)
                .SumAsync(p => p.Amount);
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            return await _dbContext.Payments
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByCertainDateAsync(DateTime date)
        {
            return await _dbContext.Payments
                                    .AsNoTracking()
                                    .Where(x => x.PaidAt == date)
                                    .ToListAsync();

        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _dbContext.Payments
                                    .AsNoTracking()
                                    .Where(x => x.UserId == userId)
                                    .OrderByDescending(x => x.PaidAt)
                                    .ToListAsync();
        }
    }
}
