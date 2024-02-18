using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteShare.Core.Services;
using NoteShare.Data.Entities;
using NoteShare.Models.Auth;

namespace NoteShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> FoodWasteReducerUserRegister([FromBody] RegisterDto registerDto)
        {
            await _authService.Register(registerDto);
            return Ok();
        }

        // POST: api/Auth/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await _authService.Login(loginDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);
        }
    }
}
