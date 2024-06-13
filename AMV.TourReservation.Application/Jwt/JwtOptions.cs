using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Application.Jwt
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
