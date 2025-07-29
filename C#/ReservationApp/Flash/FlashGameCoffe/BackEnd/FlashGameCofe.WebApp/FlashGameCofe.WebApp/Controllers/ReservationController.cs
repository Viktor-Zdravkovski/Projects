using System.Security.Claims;
using FlashGameCofe.Dto.ReservationDto;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlashGameCofe.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("GetAllReservations")]
        public async Task<ActionResult<List<ReservationsDto>>> GetAllReservations()
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
        public async Task<ActionResult<ReservationsDto>> GetReservationById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationById(id);
                if (reservation == null)
                {
                    return BadRequest("Reservation not found");
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservation([FromBody] AddReservationsDto addReservationsDto)
        {
            try
            {
                if (addReservationsDto == null)
                {
                    return BadRequest("Reservation data is required");
                }

                await _reservationService.AddReservation(addReservationsDto);
                return Ok("Reservation added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured contact admin");
            }
        }
    }
}