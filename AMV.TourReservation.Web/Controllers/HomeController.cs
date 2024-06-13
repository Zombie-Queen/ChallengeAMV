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
using AMV.TourReservation.Web.Service;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly TokenProvider _tokenProvider;

    public HomeController(IHttpClientFactory httpClientFactory, IMapper mapper, TokenProvider tokenProvider)
    {
        _httpClient = httpClientFactory.CreateClient("TourReservationAPI");
        _mapper = mapper;
        _tokenProvider = tokenProvider;
    }

    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated)
            return View(null);
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
}
