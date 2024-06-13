using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMV.TourReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var response = await _authService.Login(userDto);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();
        }
    }
}
