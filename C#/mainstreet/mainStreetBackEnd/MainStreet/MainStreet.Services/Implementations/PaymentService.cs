using AutoMapper;
using MainStreet.DataBase.Implementations.EFImplementations;
using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Models;
using MainStreet.Dto.PaymentDto;
using MainStreet.Services.Interfaces;
using MainStreet.Shared.CustomExceptions;

namespace MainStreet.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllAsync();

            if (payments == null)
                return Enumerable.Empty<PaymentDto>();

            return _mapper.Map<IEnumerable<PaymentDto>>(payments);

        }

        public async Task<PaymentDto> GetPaymentById(int id)
        {
            var payment = await _paymentRepository.GetIdByAsync(id);

            if (payment == null)
                return null;

            var paymentDto = _mapper.Map<PaymentDto>(payment);

            return paymentDto;
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByUserId(int userId)
        {
            var payments = await _paymentRepository.GetPaymentsByUserIdAsync(userId);

            if (payments == null)
                return Enumerable.Empty<PaymentDto>();

            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByDate(DateTime date)
        {
            var payments = await _paymentRepository.GetPaymentsByCertainDateAsync(date);

            if (payments == null)
                return Enumerable.Empty<PaymentDto>();

            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<IEnumerable<PaymentDto>> GetMounthlyRevenue(DateTime date)
        {
            var payments = await _paymentRepository.GetTotalRevenueByDateAsync(date);

            if (payments == null)
                return Enumerable.Empty<PaymentDto>();

            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task AddPayment(AddPaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            await _paymentRepository.AddAsync(payment);
        }
        public async Task UpdatePayment(int id, UpdatePaymentDto paymentDto)
        {
            var existingPayment = await _paymentRepository.GetIdByAsync(id);

            if (existingPayment == null)
            {
                throw new NotFoundException("No order found to update");
            }

            await _paymentRepository.UpdateAsync(existingPayment);
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _paymentRepository.GetIdByAsync(id);

            if (payment == null)
            {
                throw new NotFoundException($"Order with ID:{id} was not found");
            }

            await _paymentRepository.DeleteAsync(payment);
        }
    }
}
