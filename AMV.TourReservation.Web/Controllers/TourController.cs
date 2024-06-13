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
using System.Net;
using Microsoft.AspNetCore.Authorization;
using AMV.TourReservation.Web.Service;

public class TourController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly TokenProvider _tokenProvider;

    public TourController(IHttpClientFactory httpClientFactory, IMapper mapper, TokenProvider tokenProvider)
    {
        _httpClient = httpClientFactory.CreateClient("TourReservationAPI");
        _mapper = mapper;
        this._tokenProvider = tokenProvider;
    }

    [Authorize]
    public async Task<IActionResult> GetTours()
    {
        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept", "application/json");
        var token = _tokenProvider.GetToken();
        message.Headers.Add("Authorization", "Bearer " + token);
        var uri = _httpClient.BaseAddress.ToString() + "api/tour";
        message.RequestUri = new Uri(uri);
        message.Method = HttpMethod.Get;


        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var tours = JsonConvert.DeserializeObject<List<Tour>>(json);
            return View(_mapper.Map<List<TourViewModel>>(tours));
        }
        return View(new List<TourViewModel>());
    }

    [Authorize]
    public async Task<IActionResult> DeleteTour(int id)
    {
        try
        {
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            var token = _tokenProvider.GetToken();
            message.Headers.Add("Authorization", "Bearer " + token);
            var uri = _httpClient.BaseAddress.ToString() + $"api/tour/{id}";
            message.RequestUri = new Uri(uri);
            message.Method = HttpMethod.Delete;


            var response = await _httpClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
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

    [Authorize]
    public async Task<IActionResult> AddTour([FromBody] TourViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        HttpRequestMessage message = new HttpRequestMessage();
        message.Headers.Add("Accept", "application/json");
        var token = _tokenProvider.GetToken();
        message.Headers.Add("Authorization", "Bearer " + token);
        var uri = _httpClient.BaseAddress.ToString() + "api/tour";
        message.RequestUri = new Uri(uri);
        message.Method = HttpMethod.Post;
        message.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

}
