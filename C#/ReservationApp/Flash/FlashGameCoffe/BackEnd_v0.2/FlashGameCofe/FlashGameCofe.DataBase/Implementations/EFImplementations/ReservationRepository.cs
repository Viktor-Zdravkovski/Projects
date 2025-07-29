using FlashGameCofe.DataBase.Context;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.DataBase.Implementations.EFImplementations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly FlashGameCofeDbContext _dbContext;

        public ReservationRepository(FlashGameCofeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _dbContext.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Reservation entity)
        {
            _dbContext.Reservations.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation entity)
        {
            _dbContext.Reservations.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
        {
            return await _dbContext.Reservations
                                   .Include(x => x.User)
                                   .Where(x => x.StartingTime.Date == date)
                                   .ToListAsync();
        }

        public async Task<Reservation?> GetLatestReservationByUserId(int userId)
        {
            return await _dbContext.Reservations
                                   .Where(r => r.UserId == userId)
                                   .OrderByDescending(r => r.StartingTime)
                                   .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsWithUsers()
        {
            return await _dbContext.Reservations
                                   .Include(r => r.User)
                                   .ToListAsync();
        }

        public async Task<List<string>> GetReservedSlotsByDateAsync(DateTime date)
        {
            var formattedDate = date.Date;

            var reservedSlots = await _dbContext.Reservations
                                                .Where(x => x.StartingTime.Date == formattedDate)
                                                .Select(x => new
                                                {
                                                    x.StartingTime
                                                })
                                                .ToListAsync();

            var timeSlots = reservedSlots.Select(x => $"{x.StartingTime:HH:mm}").ToList();

            return timeSlots;
        }
    }
}
