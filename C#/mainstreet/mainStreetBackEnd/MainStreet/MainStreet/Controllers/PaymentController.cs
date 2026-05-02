using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Models;
using MainStreet.Dto.PaymentDto;
using MainStreet.Services.Interfaces;
using MainStreet.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainStreet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserRepository _userRepository;

        public PaymentController(IPaymentService paymentService, IUserRepository userRepository)
        {
            _paymentService = paymentService;
            _userRepository = userRepository;
        }

        [HttpGet("GetAllPayment")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var payments = await _paymentService.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPaymentById/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var paymnet = await _paymentService.GetPaymentById(id);
                return Ok(paymnet);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Payment with ID:{id} was not found");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPaymentByUserId/{userId}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetPaymentByUserId(int userId)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var paymnet = await _paymentService.GetPaymentsByUserId(userId);
                return Ok(paymnet);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Payment with ID:{userId} was not found");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPaymentsByDate")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetPaymentsByDate([FromQuery] DateTime date)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var paymnet = await _paymentService.GetPaymentsByDate(date);
                return Ok(paymnet);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"No payments for date: {date}");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMounthlyRevenue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMounthlyRevenue([FromQuery] DateTime date)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin")?.Value;
                if (role == null || role != "Admin")
                {
                    return Forbid();
                }

                var paymnet = await _paymentService.GetMounthlyRevenue(date);
                return Ok(paymnet);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"No payments for this month: {date}");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPayment([FromBody] AddPaymentDto addPaymentDto)
        {
            try
            {
                if (addPaymentDto == null)
                {
                    return BadRequest("Reservation data is required");
                }

                await _paymentService.AddPayment(addPaymentDto);
                return Ok(new { message = "Reservation added successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment([FromBody] int id, UpdatePaymentDto updatePaymentDto)
        {
            if (updatePaymentDto == null)
                return BadRequest("Invalid reservation data.");

            try
            {
                await _paymentService.UpdatePayment(id, updatePaymentDto);
                return Ok("Reservation updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await _paymentService.DeletePayment(id);
                return Ok($"The payment with ID: {id} was successfully deleted");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Payment with ID:{id} was not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
