using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Application.Jwt;
using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, JwtGenerator jwtGenerator, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._jwtGenerator = jwtGenerator;
            this._mapper = mapper;
        }

        public async Task<LoginResponseDto?> Login(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (_userRepository.CheckCredentials(user))
            {
                var token = _jwtGenerator.GenerateToken(user);
                return new LoginResponseDto()
                {
                    User = userDto,
                    Token = token
                };
            }
            return null;
        }
    }
}
