using HotelManagement.Dto.ReservationsDto;
using HotelManagement.Services.Implementations;
using HotelManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelManagementApp.Controllers
{
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
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var allReservations = await _reservationService.GetAllReservations();
                return Ok(allReservations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetReservationById/{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            try
            {
                var reservationById = await _reservationService.GetReservationById(id);
                return Ok(reservationById);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetReservationByUser/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(int id)
        {
            try
            {

                var reservationsByUser = await _reservationService.GetReservationsByUser(id);
                return Ok(reservationsByUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("CheckAvailability")]
        public async Task<IActionResult> CheckAvailability(DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var datesCheck = await _reservationService.CheckAvailability(checkIn, checkOut);
                return Ok(datesCheck);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservation(AddReservationDto addReservationDto)
        {
            try
            {
                await _reservationService.AddReservation(addReservationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(int id, UpdateReservationDto updateReservationDto)
        {
            try
            {
                await _reservationService.UpdateReservation(id, updateReservationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpDelete("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationService.CancelReservation(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

    }
}
