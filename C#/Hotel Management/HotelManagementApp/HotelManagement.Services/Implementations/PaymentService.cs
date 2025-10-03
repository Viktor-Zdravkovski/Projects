using AutoMapper;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using HotelManagement.Dto.PaymentsDto;
using HotelManagement.Services.Interfaces;
using HotelManagement.Shared.CustomExceptions;

namespace HotelManagement.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IReservationRepository reservationRepository, IUserRepository userRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPayments()
        {
            var allPayments = await _paymentRepository.GetAllAsync();
            if (!allPayments.Any())
                return Enumerable.Empty<PaymentDto>();

            return _mapper.Map<IEnumerable<PaymentDto>>(allPayments);
        }

        public async Task<PaymentDto> GetPaymentByReservation(int id)
        {
            var payment = await _paymentRepository.GetPaymentByReservationIdAsync(id);
            if (payment == null)
                throw new NotFoundException("No payment for this reservation");

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByUser(int id)
        {
            var payments = await _paymentRepository.GetPaymentsByUserAsync(id);
            if (!payments.Any())
                throw new NotFoundException("No payments found for this user");

            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task AddPayment(AddPaymentDto addPaymentDto)
        {
            var payment = _mapper.Map<Payment>(addPaymentDto);
            await _paymentRepository.AddAsync(payment);
        }

        public async Task UpdatePaymentStatus(int id, PaymentStatus paymentStatus)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(id);
            if (existingPayment == null)
                throw new NotFoundException("Payment not found.");

            Dictionary<PaymentStatus, List<PaymentStatus>> allowedTransitions = new Dictionary<PaymentStatus, List<PaymentStatus>>
            {
                { PaymentStatus.Pending, new List<PaymentStatus> { PaymentStatus.Completed, PaymentStatus.Canceled, PaymentStatus.Failed } },
                { PaymentStatus.Completed, new List<PaymentStatus>() },
                { PaymentStatus.Canceled, new List<PaymentStatus>() },
                { PaymentStatus.Failed, new List<PaymentStatus> { PaymentStatus.Pending, PaymentStatus.Canceled } }
            };

            var currentStatus = existingPayment.Status;

            if (!allowedTransitions[currentStatus].Contains(paymentStatus))
                throw new InvalidPaymentStatusTransitionException($"Cannot change status from {currentStatus} to {paymentStatus}");

            existingPayment.Status = paymentStatus;

            await _paymentRepository.UpdateAsync(existingPayment);
        }

        public async Task CancelPayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException("Payment not found");

            await _paymentRepository.DeleteAsync(payment.Id);
        }
    }
}
