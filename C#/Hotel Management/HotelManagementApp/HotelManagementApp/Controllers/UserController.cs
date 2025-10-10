using HotelManagement.Domain.Enums;
using HotelManagement.Dto.UsersDto;
using HotelManagement.Services.Interfaces;
using HotelManagement.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetUserById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetUserByRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByRole(Roles role)
        {
            try
            {
                var usersByRole = await _userService.GetUsersByRole(role);
                return Ok(usersByRole);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            try
            {
                var updatedPassword = _userService.UpdatePassword(userId, oldPassword, newPassword);
                return Ok("The password was successfuly updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin")
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LogInUserDto logInUserDto)
        {
            try
            {
                LogInResponseDto token = await _userService.LogInUser(logInUserDto);
                return Ok(token);
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                await _userService.RegisterUser(registerUserDto);
                return Ok("User was successfully created");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }
    }
}
