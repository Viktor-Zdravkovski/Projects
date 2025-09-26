using HotelManagement.DataBase.Context;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataBase.Implementations.EFImplementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HotelManagementDbContext _context;

        public PaymentRepository(HotelManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payment.ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payment.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Payment entity)
        {
            await _context.Payment.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            var existing = await _context.Payment.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Payment with Id {entity.Id} not found.");

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paymentToDelete = await _context.Payment.FirstOrDefaultAsync(x => x.Id == id);

            if (paymentToDelete == null)
                throw new KeyNotFoundException($"Payment with Id {id} not found.");

            _context.Payment.Remove(paymentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetByReservationIdAsync(int reservationId)
        {
            return await _context.Payment.FirstOrDefaultAsync(x => x.ReservationId == reservationId);
        }


    }
}
