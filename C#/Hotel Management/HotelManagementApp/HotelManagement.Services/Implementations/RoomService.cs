using AutoMapper;
using HotelManagement.DataBase.Implementations.EFImplementations;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Enums;
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

        public async Task<IEnumerable<RoomDto>> GetRoomsByStatus(RoomStatus roomStatus)
        {
            var rooms = await _roomRepository.GetRoomsByStatus(roomStatus);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByType(string type)
        {
            var roomsByType = await _roomRepository.GetAllAsync();
            roomsByType.Where(x => x.Type == type);

            return _mapper.Map<IEnumerable<RoomDto>>(roomsByType);
        }

        public async Task ChangeRoomStatus(int roomId, RoomStatus roomStatus)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new NotFoundException("No room found.");

            if (room.Status == roomStatus)
                throw new ArgumentException("Cant update the same status of a room.");

            Dictionary<RoomStatus, List<RoomStatus>> allowedTransitions = new Dictionary<RoomStatus, List<RoomStatus>>
            {
                {RoomStatus.Available, new List<RoomStatus> {RoomStatus.Occupied, RoomStatus.Maintenance} },
                {RoomStatus.Occupied, new List<RoomStatus> {RoomStatus.Available } },
                {RoomStatus.Maintenance, new List<RoomStatus>{ RoomStatus.Available} },
            };

            var currentStatus = room.Status;

            if (!allowedTransitions[currentStatus].Contains(roomStatus))
                throw new InvalidOperationException($"Cannot change status from {currentStatus} to {roomStatus}");

            room.Status = roomStatus;

            await _roomRepository.UpdateAsync(room);
        }

        public async Task AddRoom(AddRoomDto roomDto)
        {
            var addedRoom = _mapper.Map<Room>(roomDto);
            await _roomRepository.AddAsync(addedRoom);
        }

        public async Task UpdateRoom(int id, UpdateRoomDto roomDto)
        {
            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null)
                throw new NotFoundException("No room found.");

            if (existingRoom.Status == roomDto.Status &&
                existingRoom.PricePerNight == roomDto.PricePerNight &&
                existingRoom.Type == roomDto.Type)
                return;


            //existingRoom.Status = roomDto.Status;
            //existingRoom.PricePerNight = roomDto.PricePerNight;
            //existingRoom.Type = roomDto.Type;

            _mapper.Map(roomDto, existingRoom);

            await _roomRepository.UpdateAsync(existingRoom);
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new NotFoundException("No room found");

            if ((room.Status == RoomStatus.Occupied || room.Status == RoomStatus.Maintenance) || (room.Reservations.Any(x => x.CheckedOut >= DateTime.Today)))
                throw new InvalidOperationException("Cannot delete an occupied room.");

            await _roomRepository.DeleteAsync(room.Id);
        }
    }
}
