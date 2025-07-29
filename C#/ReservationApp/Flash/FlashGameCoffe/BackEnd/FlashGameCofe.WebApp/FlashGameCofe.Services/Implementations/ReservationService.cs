using AutoMapper;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.ReservationDto;
using FlashGameCofe.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IRepository<Reservation> reservationService, IMapper mapper)
        {
            _reservationRepository = reservationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationsDto>> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationsDto>>(reservations);
        }

        public async Task<ReservationsDto> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation == null)
                return null;

            return _mapper.Map<ReservationsDto>(reservation);
        }

        public async Task AddReservation(AddReservationsDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(updateReservationDto.Id);

            if (existingReservation == null)
                return;

            if (existingReservation.User == null)
                existingReservation.User = new User();

            if (!string.IsNullOrEmpty(updateReservationDto.FirstName))
                existingReservation.User.FirstName = updateReservationDto.FirstName;

            if (!string.IsNullOrEmpty(updateReservationDto.LastName))
                existingReservation.User.LastName = updateReservationDto.LastName;

            if (!string.IsNullOrEmpty(updateReservationDto.Email))
                existingReservation.User.Email = updateReservationDto.Email;

            if (!string.IsNullOrEmpty(updateReservationDto.PhoneNumber))
                existingReservation.User.PhoneNumber = updateReservationDto.PhoneNumber;

            if (updateReservationDto.Note != null)
                existingReservation.Note = _mapper.Map<Note>(updateReservationDto.Note);

            await _reservationRepository.UpdateAsync(existingReservation);
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation == null)
                return;

            await _reservationRepository.DeleteAsync(reservation);
        }
    }
}
