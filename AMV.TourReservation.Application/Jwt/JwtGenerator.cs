using AMV.TourReservation.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMV.TourReservation.Application.Jwt
{
    public class JwtGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtGenerator(IOptions<JwtOptions> jwtOptions)
        {
            this._jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
