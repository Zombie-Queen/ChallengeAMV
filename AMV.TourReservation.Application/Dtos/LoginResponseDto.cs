using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Application.Dtos
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
