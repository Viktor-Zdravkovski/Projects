using FlashGameCofe.Dto.UserDtos;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlashGameCofe.Controllers
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
                return StatusCode(StatusCodes.Status201Created, "User was successfully created");
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
