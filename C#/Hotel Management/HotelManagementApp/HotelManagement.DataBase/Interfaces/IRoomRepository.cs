using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;

namespace HotelManagement.DataBase.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);

        Task<Room> GetByRoomNumberAsync(string roomNumber);

        Task<IEnumerable<Room>> GetRoomsByStatus(RoomStatus roomStatus);
    }
}
