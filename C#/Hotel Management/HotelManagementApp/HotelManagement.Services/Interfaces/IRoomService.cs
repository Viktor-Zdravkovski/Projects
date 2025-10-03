using HotelManagement.Dto.RoomsDto;

namespace HotelManagement.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRooms();

        Task<RoomDto> GetRoomById(int id);

        Task ChangeRoomStaus(int roomId);

        Task<IEnumerable<RoomDto>> GetRoomsByType();

        Task<IEnumerable<RoomDto>> GetRoomsByStatus();

        Task AddRoom(AddRoomDto roomDto);

        Task UpdateRoom(int id, UpdateRoomDto roomDto);

        Task DeleteRoom(int id);
    }
}
