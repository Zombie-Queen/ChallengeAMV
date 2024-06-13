using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Domain.Entities;
using AMV.TourReservation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Infrastructure.Repositories
{
    public class TourRepository : GenericRepository<Tour>, ITourRepository 
    {
        public TourRepository(TourReservationDbContext context) : base(context)
        {
        }
    }
}
