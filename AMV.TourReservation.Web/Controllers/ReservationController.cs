using AutoMapper;
using AMV.TourReservation.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AMV.TourReservation.Web.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using AMV.TourReservation.Web.Service;
using Microsoft.AspNetCore.Authorization;

public class ReservationController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly TokenProvider _tokenProvider;

    public ReservationController(IHttpClientFactory httpClientFactory, IMapper mapper, TokenProvider tokenProvider)
    {
        _httpClient = httpClientFactory.CreateClient("TourReservationAPI");
        _mapper = mapper;
        _tokenProvider = tokenProvider;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept", "application/json");
        var token = _tokenProvider.GetToken();
        message.Headers.Add("Authorization", "Bearer " + token);
        var uri = _httpClient.BaseAddress.ToString() + "api/reservation";
        message.RequestUri = new Uri(uri);
        message.Method = HttpMethod.Get;


        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);
            return View(_mapper.Map<List<ReservationViewModel>>(reservations));
        }
        return View(new List<ReservationViewModel>());
    }

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetAllReservations()
    {
        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept", "application/json");
        var token = _tokenProvider.GetToken();
        message.Headers.Add("Authorization", "Bearer " + token);
        var uri = _httpClient.BaseAddress.ToString() + "api/reservation";
        message.RequestUri = new Uri(uri);
        message.Method = HttpMethod.Get;


        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var reservations = _mapper.Map<List<ReservationViewModel>>(JsonConvert.DeserializeObject<List<Reservation>>(json));
            return View(reservations);
        }
        return View(new List<ReservationViewModel>());
    }

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetReservationsByTour(int id)
    {
        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept", "application/json");
        var token = _tokenProvider.GetToken();
        message.Headers.Add("Authorization", "Bearer " + token);
        var uri = _httpClient.BaseAddress.ToString() + $"api/reservation/GetReservationsByTour/{id}";
        message.RequestUri = new Uri(uri);
        message.Method = HttpMethod.Get;


        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var reservations = _mapper.Map<List<ReservationViewModel>>(JsonConvert.DeserializeObject<List<Reservation>>(json));            

            return View("Index", reservations);
        }
        return View(new List<ReservationViewModel>());
    }

    [Authorize]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        try
        {
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            var token = _tokenProvider.GetToken();
            message.Headers.Add("Authorization", "Bearer " + token);
            var uri = _httpClient.BaseAddress.ToString() + $"api/reservation/{id}";
            message.RequestUri = new Uri(uri);
            message.Method = HttpMethod.Delete;


            var response = await _httpClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReservation([FromBody] ReservationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept", "application/json");
        var token = _tokenProvider.GetToken();
        message.Headers.Add("Authorization", "Bearer " + token);
        var uri = _httpClient.BaseAddress.ToString() + "api/reservation";
        message.RequestUri = new Uri(uri);
        message.Method = HttpMethod.Post;
        message.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            return Ok(response);
        }

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

}
