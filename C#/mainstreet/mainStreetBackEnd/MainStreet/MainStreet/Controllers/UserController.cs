using MainStreet.Dto.UserDto;
using MainStreet.Services.Interfaces;
using MainStreet.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainStreet.Controllers
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

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUser([FromBody] LogInUserDto logInUserDto)
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                await _userService.RegisterUser(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, ("User was created successfuly"));
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
    }
}
