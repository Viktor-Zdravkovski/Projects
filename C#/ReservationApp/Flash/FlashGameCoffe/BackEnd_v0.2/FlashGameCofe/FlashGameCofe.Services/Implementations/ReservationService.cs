using AutoMapper;
using FlashGameCofe.DataBase.Implementations.EFImplementations;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.ReservationsDto;
using FlashGameCofe.Dto.UserDtos;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.CustomExceptions;

namespace FlashGameCofe.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllReservationsWithUsers();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                return null;

            var user = await _userRepository.GetByIdAsync(reservation.UserId);

            if (user == null)
                return null;

            var reservationDto = new ReservationDto
            {
                Id = reservation.Id,
                NoteDescription = reservation.NoteDescription,
                StartingTime = reservation.StartingTime,
                UserId = reservation.UserId,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                }
            };

            return reservationDto;
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByUserIdAsync(int userId)
        {
            var reservations = await _reservationRepository.GetReservationsByUserIdAsync(userId);

            var reservationDtos = reservations.Select(x => new ReservationDto
            {
                Id = x.Id,
                UserId = x.UserId,
                StartingTime = x.StartingTime,
                NoteDescription = x.NoteDescription
            }).ToList();

            return reservationDtos;
        }

        public async Task AddReservation(AddReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(updateReservationDto.Id);

            if (existingReservation == null)
            {
                throw new NotFoundException("No reservation found to update");
            }

            existingReservation.StartingTime = updateReservationDto.StartingTime.Value;
            existingReservation.NoteDescription = updateReservationDto.NoteDescription;

            await _reservationRepository.UpdateAsync(existingReservation);
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation == null)
            {
                throw new NotFoundException($"Reservation with ID:{id} was not found");
            }

            await _reservationRepository.DeleteAsync(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetReservatiosByDate(DateTime date)
        {
            var dateReservations = await _reservationRepository.GetByDateAsync(date);

            foreach (var reservation in dateReservations)
            {
                reservation.StartingTime = reservation.StartingTime.ToLocalTime();
            }

            return _mapper.Map<IEnumerable<ReservationDto>>(dateReservations);
        }

        public async Task<bool> AddOrUpdateReservation(AddReservationDto dto, int userId)
        {
            var existingReservation = await _reservationRepository.GetLatestReservationByUserId(userId);

            if (existingReservation == null)
            {
                var newReservation = new Reservation
                {
                    UserId = userId,
                    StartingTime = dto.StartingTime,
                    NoteDescription = dto.NoteDescription
                };

                await _reservationRepository.AddAsync(newReservation);
            }
            else
            {
                existingReservation.StartingTime = dto.StartingTime;
                existingReservation.NoteDescription = dto.NoteDescription;

                await _reservationRepository.UpdateAsync(existingReservation);
            }

            return true;
        }

        public async Task<List<string>> GetReservedSlotsByDateAsync(DateTime date)
        {
            return await _reservationRepository.GetReservedSlotsByDateAsync(date);
        }
    }
}
