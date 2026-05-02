using HotelManagement.Domain.Enums;
using HotelManagement.Dto.PaymentsDto;

namespace HotelManagement.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPayments();

        Task<IEnumerable<PaymentDto>> GetPaymentsByUser(int id);

        Task AddPayment(AddPaymentDto addPaymentDto);

        Task<PaymentDto> GetPaymentByReservation(int id);

        Task UpdatePaymentStatus(int id, PaymentStatus paymentStatus);

        Task CancelPayment(int id);

    }
}
