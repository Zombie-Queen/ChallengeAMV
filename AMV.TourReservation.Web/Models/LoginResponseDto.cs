using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Web.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
