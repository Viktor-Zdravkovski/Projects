using HotelManagement.Domain.Enums;
using HotelManagement.Dto.RoomsDto;
using HotelManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using XAct.Security;

namespace HotelManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetAllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                var allRooms = await _roomService.GetAllRooms();
                return Ok(allRooms);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetRoomById/{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            try
            {
                var roomById = await _roomService.GetRoomById(id);
                return Ok(roomById);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetRoomByStatus")]
        public async Task<IActionResult> RoomByStatus(RoomStatus status)
        {
            try
            {
                var roomByStatus = await _roomService.GetRoomsByStatus(status);
                return Ok(roomByStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetRoomsByType")]
        public async Task<IActionResult> GetRoomByType(string type)
        {
            try
            {
                var roomByType = await _roomService.GetRoomsByType(type);
                return Ok(roomByType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPut("UpdateRoomStatus")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> ChangeRoomStatus(int roomId, RoomStatus status)
        {
            try
            {
                var changedRoomStatus = _roomService.ChangeRoomStatus(roomId, status);
                return Ok(changedRoomStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPost("AddRoom")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> AddRoom(AddRoomDto addRoomDto)
        {
            try
            {
                await _roomService.AddRoom(addRoomDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPut("UpdateRoom")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDto updateRoomDto)
        {
            try
            {
                await _roomService.UpdateRoom(id, updateRoomDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpDelete("DeleteRoom/{id}")]
        [Authorization(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _roomService.DeleteRoom(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }
    }
}
