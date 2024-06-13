using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tour, TourDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
