using HotelManagement.DataBase.Context;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HotelManagement.DataBase.Implementations.EFImplementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelManagementDbContext _context;

        public RoomRepository(HotelManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Room entity)
        {
            await _context.Rooms.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room entity)
        {
            var existing = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Room with Id {entity.Id} not found. ");

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var roomToDelete = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (roomToDelete == null)
                throw new KeyNotFoundException($"Room with Id {id} not found. ");
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
        {
            var allRooms = await _context.Rooms.ToListAsync();

            var RoomOfIdsOfAllRooms = allRooms.Select(x => x.Id);

            //var todaysReservation = await _context.Reservations
            //                       .Where(x => x.CheckedIn >= checkIn && x.CheckedIn < checkOut || x.CheckedIn < checkOut && x.CheckedOut > checkIn)
            //                       .ToListAsync();

            var listOfIdsOfOccupiedRooms = await _context.Reservations.Where(x => x.CheckedIn < checkOut && x.CheckedOut > checkIn).Select(x => x.RoomId).Distinct().ToListAsync();

            var listOfIdsOfFreeRooms = RoomOfIdsOfAllRooms.Except(listOfIdsOfOccupiedRooms).ToList();

            var finale = allRooms.Where(x => listOfIdsOfFreeRooms.Contains(x.Id));

            return finale;
        }


        public async Task<Room> GetByRoomNumberAsync(string roomNumber)
        {
            if (roomNumber == null)
                throw new KeyNotFoundException($"Room with number {roomNumber} not found.");

            var numberOfRoom = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomNumber.Trim().ToLower() == roomNumber.Trim().ToLower());

            if (numberOfRoom == null)
                throw new KeyNotFoundException($"Room with number {roomNumber} not found.");


            return numberOfRoom;
        }

        public async Task<IEnumerable<Room>> GetRoomsByStatus(RoomStatus roomStatus)
        {
            return await _context.Rooms.Where(x => x.Status == roomStatus).ToListAsync();
        }
    }
}
