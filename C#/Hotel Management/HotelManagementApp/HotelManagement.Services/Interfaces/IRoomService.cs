using HotelManagement.Domain.Enums;
using HotelManagement.Dto.RoomsDto;

namespace HotelManagement.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRooms();

        Task<RoomDto> GetRoomById(int id);

        Task ChangeRoomStatus(int roomId, RoomStatus roomStatus);

        Task<IEnumerable<RoomDto>> GetRoomsByType(string type);

        Task<IEnumerable<RoomDto>> GetRoomsByStatus(RoomStatus roomStatus);

        Task AddRoom(AddRoomDto roomDto);

        Task UpdateRoom(int id, UpdateRoomDto roomDto);

        Task DeleteRoom(int id);
    }
}
