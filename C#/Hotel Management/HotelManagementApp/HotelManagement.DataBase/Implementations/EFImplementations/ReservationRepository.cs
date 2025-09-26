using HotelManagement.DataBase.Context;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataBase.Implementations.EFImplementations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelManagementDbContext _context;

        public ReservationRepository(HotelManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Reservation entity)
        {
            await _context.Reservations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            var existing = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Reservation with Id {entity.Id} not found.");

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservationToDelete = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            if (reservationToDelete == null)
                throw new KeyNotFoundException($"Reservation with Id {id} not found.");

            _context.Remove(reservationToDelete);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            DateTime todaysDate = DateTime.Now.Date;
            DateTime start = todaysDate.Date;
            DateTime end = start.AddDays(1);

            var todaysReservation = await _context.Reservations.Where(x => x.CheckedIn >= start && x.CheckedIn < end).ToListAsync();

            return todaysReservation ?? new List<Reservation>();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId)
        {
            var reservationByUser = await _context.Reservations.Where(x => x.UserId == userId).ToListAsync();

            return reservationByUser ?? new List<Reservation>();
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var hasConflicts = await _context.Reservations.Where(x => x.RoomId == roomId).Where(r => r.CheckedIn < checkOut && r.CheckedOut > checkIn).AnyAsync();

            if (!hasConflicts)
                return true;

            return false;
        }
    }
}
