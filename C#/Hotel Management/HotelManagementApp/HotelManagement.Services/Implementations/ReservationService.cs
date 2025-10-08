using AutoMapper;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Models;
using HotelManagement.Dto.PaymentsDto;
using HotelManagement.Dto.ReservationsDto;
using HotelManagement.Dto.RoomsDto;
using HotelManagement.Dto.UsersDto;
using HotelManagement.Services.Interfaces;
using HotelManagement.Shared.CustomExceptions;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository,
            IRoomRepository roomRepository, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservations()
        {
            var allReservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(allReservations);
        }

        public async Task<ReservationDto> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException("Reservation not found.");

            var user = await _userRepository.GetByIdAsync(reservation.UserId);
            if (user == null)
                throw new NotFoundException("User not found.");

            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException("Payment not found.");

            var room = await _roomRepository.GetByIdAsync(reservation.RoomId);
            if (room == null)
                throw new NotFoundException("Room not found.");

            var reservationDto = new ReservationDto
            {
                CheckedIn = reservation.CheckedIn,
                CheckedOut = reservation.CheckedOut,
                Payment = reservation.Payment.Method,
                Room = new RoomDto
                {
                    PricePerNight = room.PricePerNight,
                    RoomNumber = room.RoomNumber,
                    Status = room.Status,
                    Type = room.Type
                },
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                }
            };
            return reservationDto;
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByUser(int userId)
        {
            var reservations = await _reservationRepository.GetReservationsByUserAsync(userId);

            var reservationDto = reservations.Select(x => new ReservationDto
            {
                User = new UserDto
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    PhoneNumber = x.User.PhoneNumber
                },
                Payment = x.Payment.Method,
                CheckedIn = x.CheckedIn,
                CheckedOut = x.CheckedOut,
            });

            return reservationDto;
        }

        public async Task<IEnumerable<RoomDto>> CheckAvailability(DateTime checkIn, DateTime checkOut)
        {
            if (checkIn >= checkOut)
                throw new Exception("Check in can't be greater than check out");

            var allRooms = await _roomRepository.GetAvailableRoomsAsync(checkIn, checkOut);

            return _mapper.Map<IEnumerable<RoomDto>>(allRooms);
        }

        public async Task AddReservation(AddReservationDto addReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(addReservationDto);
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservation(int id, UpdateReservationDto updateReservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation == null)
                throw new NotFoundException("Reservation not found.");

            if (updateReservationDto.CheckedIn >= updateReservationDto.CheckedOut)
                throw new ValidationException("Check-in must be earlier than check-out.");

            if (updateReservationDto.CheckedIn == existingReservation.CheckedIn)
                throw new ValidationException("Check-in cant be on the same date.");

            var isRoomAvaialbe = await _reservationRepository
                                .IsRoomAvailableAsync(existingReservation.RoomId, updateReservationDto.CheckedIn, updateReservationDto.CheckedOut, id);

            if (!isRoomAvaialbe)
                throw new ConflictException("The room is unavailable.");

            existingReservation.CheckedIn = updateReservationDto.CheckedIn;
            existingReservation.CheckedOut = updateReservationDto.CheckedOut;

            await _reservationRepository.UpdateAsync(existingReservation);
        }

        public async Task CancelReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation == null)
                throw new NotFoundException("Reservation not found");

            await _reservationRepository.DeleteAsync(reservation.Id);
        }
    }
}
