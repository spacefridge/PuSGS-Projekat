using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuSGSProjekat.DTO.LoginDTO;
using PuSGSProjekat.DTO.UserDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.Interfaces;
using System;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;

namespace PuSGSProjekat.Controllers
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
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
            UserResponseDTO user;

            try
            {
                user = _userService.GetUserById(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegistrationRequestDTO requestDto)
        {
            UserResponseDTO user;

            try
            {
                user = _userService.RegisterUser(requestDto);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }   

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, [FromBody] UserEditRequestDTO requestDto)
        {
            if (!User.HasClaim("Id", id.ToString()))
            {
                return Forbid();
            }

            UserResponseDTO user;

            try
            {
                user = _userService.UpdateUser(id, requestDto);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginRequestDTO requestDto)
        {
            LoginResponseDTO responseDto;

            try
            {
                responseDto = _userService.LoginUser(requestDto);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(responseDto);
        }

        [HttpPost("verify/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult VerifyUser(long id, [FromBody] VerificationRequestDTO requestDto)
        {
            VerificationResponseDTO user;

            try
            {
                user = _userService.VerifyUser(id, requestDto);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }
    }
}
