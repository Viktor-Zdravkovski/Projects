using AutoMapper;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Models;
using HotelManagement.Dto.RoomsDto;
using HotelManagement.Services.Interfaces;
using HotelManagement.Shared.CustomExceptions;

namespace HotelManagement.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRooms()
        {
            var allRooms = await _roomRepository.GetAllAsync();
            if (allRooms == null)
                return Enumerable.Empty<RoomDto>();

            return _mapper.Map<IEnumerable<RoomDto>>(allRooms);
        }

        public async Task<RoomDto> GetRoomById(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new NotFoundException("No room found.");

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByStatus()
        {

        }

        public Task<IEnumerable<RoomDto>> GetRoomsByType()
        {
            throw new NotImplementedException();
        }

        public Task ChangeRoomStaus(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task AddRoom(AddRoomDto roomDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoom(int id, UpdateRoomDto roomDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoom(int id)
        {
            throw new NotImplementedException();
        }
    }
}
