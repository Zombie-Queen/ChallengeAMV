using AMV.TourReservation.Domain.Entities;
using AMV.TourReservation.Web.Models;
using AMV.TourReservation.Web.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly TokenProvider _tokenProvider;

        public AuthController(IHttpClientFactory httpClientFactory, IMapper mapper, TokenProvider tokenProvider)
        {
            this._httpClientFactory = httpClientFactory;
            this._mapper = mapper;
            this._tokenProvider = tokenProvider;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            };
            using (var client = _httpClientFactory.CreateClient("TourReservationAPI")) 
            {
                var json = JsonConvert.SerializeObject(userViewModel);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(responseJson);

                    await SignInUser(loginResponse);
                    _tokenProvider.SetToken(loginResponse.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto responseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(responseDto.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type ==JwtRegisteredClaimNames.Name).Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
