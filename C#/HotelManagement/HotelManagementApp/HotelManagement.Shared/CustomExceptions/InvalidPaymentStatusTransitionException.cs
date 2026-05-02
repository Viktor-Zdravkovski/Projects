namespace HotelManagement.Shared.CustomExceptions
{
    public class InvalidPaymentStatusTransitionException : Exception
    {
        public InvalidPaymentStatusTransitionException(string message) : base(message)
        {
            
        }
}
}
