﻿using DropShipping.Dto.UserDto;
using DropShipping.Dto.UserDtos;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DropShipping.Controllers
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
        public IActionResult LoginUser([FromBody] LogInUserDto logInUserDto)
        {
            try
            {
                LogInResponseDto token = _userService.LogInUser(logInUserDto);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.RegisterUser(registerUserDto);
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