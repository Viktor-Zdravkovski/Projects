using FlashGameCofe.DataBase.Context;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.DataBase.Implementations.EFImplementations
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly FlashGameCofeDbContext _dbContext;

        public ReservationRepository(FlashGameCofeDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task AddAsync(Reservation entity)
        {
            _dbContext.Reservations.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (reservation != null)
            {
                _dbContext.Entry(reservation).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Reservation entity)
        {
            _dbContext.Reservations.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
