using MainStreet.Dto.PaymentDto;

namespace MainStreet.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPayments();

        Task<PaymentDto> GetPaymentById(int id);

        Task<IEnumerable<PaymentDto>> GetPaymentsByUserId(int userId);

        Task<IEnumerable<PaymentDto>> GetPaymentsByDate(DateTime date);

        Task<IEnumerable<PaymentDto>> GetMounthlyRevenue(DateTime date);

        Task AddPayment(AddPaymentDto paymentDto);

        Task UpdatePayment(int id, UpdatePaymentDto paymentDto);

        Task DeletePayment(int id);
    }
}
