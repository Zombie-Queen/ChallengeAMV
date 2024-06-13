using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMV.reservationReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationManagerService _rmService;

        public ReservationController(ReservationManagerService rmService)
        {
            _rmService = rmService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _rmService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("GetReservationsByTour/{id}")]
        [Authorize]
        public async Task<IActionResult> GetReservationsByTour(int id)
        {
            var reservations = await _rmService.GetReservationsByTourAsync(id);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _rmService.GetReservationAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDto reservation)
        {
            await _rmService.AddReservationAsync(reservation);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _rmService.DeleteReservationAsync(id);
            return Ok();
        }
    }
}
