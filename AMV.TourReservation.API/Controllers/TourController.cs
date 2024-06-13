using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMV.Tourtour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourController : ControllerBase
    {
        private readonly ReservationManagerService _rmService;

        public TourController(ReservationManagerService rmService)
        {
            _rmService = rmService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllTours()
        {
            var tours = await _rmService.GetAllToursAsync();
            return Ok(tours);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTourById(int id)
        {
            var tour = await _rmService.GetTourAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> AddTour([FromBody] TourDto tour)
        {
            await _rmService.AddTourAsync(tour);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTour(int id)
        {
            await _rmService.DeleteTourAsync(id);
            return Ok();
        }


    }
}