using AMV.TourReservation.Domain.Entities;
using AMV.TourReservation.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMV.TourReservation.Web.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tour, TourViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();
        }
    }
}
