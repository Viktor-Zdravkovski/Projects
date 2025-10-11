using HotelManagement.Domain.Enums;
using HotelManagement.Dto.PaymentsDto;
using HotelManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XAct.Security;

namespace HotelManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("GetAllPayments")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var allPayments = await _paymentService.GetAllPayments();
                return Ok(allPayments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetPaymentById/{id}")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentByReservation(int id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentByReservation(id);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetPaymentsByUser/{userId}")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentsByUser(int userId)
        {
            try
            {
                var payments = await _paymentService.GetPaymentsByUser(userId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("AddPayment")]
        public async Task<IActionResult> AddPayment(AddPaymentDto addPaymentDto)
        {
            try
            {
                await _paymentService.AddPayment(addPaymentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPut("UpdatePayment")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatus paymentStatus)
        {
            try
            {
                await _paymentService.UpdatePaymentStatus(id, paymentStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await _paymentService.CancelPayment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }
    }
}
