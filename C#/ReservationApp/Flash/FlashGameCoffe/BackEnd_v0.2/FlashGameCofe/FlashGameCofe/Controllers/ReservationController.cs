using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.ReservationsDto;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlashGameCofe.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        public ReservationController(IReservationService reservationService, IUserService userService)
        {
            _reservationService = reservationService;
            _userService = userService;
        }

        [HttpGet("GetAllReservations")]
        public async Task<ActionResult<List<ReservationDto>>> GetAllReservations()
        {
            try
            {
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
                Console.WriteLine(roleClaim);
                if (roleClaim == null || roleClaim != "Admin")
                {
                    return Forbid();
                }

                var reservations = await _reservationService.GetAllReservations();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetReservationById/{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationById(id);
                if (reservation == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Reservation with ID: {id} was not found");
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetReservationByUserId/{userId}")]
        public async Task<IActionResult> GetReservationByUserId(int userId)
        {
            try
            {
                var reservation = await _reservationService.GetReservationsByUserIdAsync(userId);

                return Ok(reservation);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No reservations found for this user");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetReservationForToday")]
        public async Task<IActionResult> GetReservationForToday()
        {
            try
            {
                var today = DateTime.UtcNow.Date;
                var reservations = await _reservationService.GetReservatiosByDate(today);

                if (reservations == null || !reservations.Any())
                {
                    return NotFound("No Reservations found for this date");
                }

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetReservedSlots")]
        public async Task<IActionResult> GetReservations([FromQuery] DateTime date)
        {
            var reservedSlots = await _reservationService.GetReservedSlotsByDateAsync(date);

            return Ok(new { reservedSlots });
        }

        [HttpPost("AddOrUpdate")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> AddOrUpdateReservation([FromBody] AddReservationDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _reservationService.AddOrUpdateReservation(dto, userId);

            if (result)
            {
                return Ok("Reservation successfully added or updated.");
            }

            return BadRequest("Failed to process reservation.");
        }

        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservation([FromBody] AddReservationDto addReservationsDto)
        {
            try
            {
                if (addReservationsDto == null)
                {
                    return BadRequest("Reservation data is required");
                }

                await _reservationService.AddReservation(addReservationsDto);
                return Ok(new { message = "Reservation added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation([FromBody] UpdateReservationDto updateReservationDto)
        {
            if (updateReservationDto == null)
                return BadRequest("Invalid reservation data.");

            try
            {
                await _reservationService.UpdateReservation(updateReservationDto);
                return Ok("Reservation updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpDelete("DeleteReservationById/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationService.DeleteReservation(id);
                return Ok($"The reservation with ID: {id} was successfully deleted");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Reservation with ID:{id} was not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured contact admin");
            }
        }
    }
}