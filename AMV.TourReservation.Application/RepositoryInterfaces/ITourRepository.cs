using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.RepositoryInterfaces
{
    public interface ITourRepository : IGenericRepository<Tour>
    {
        //Task<IEnumerable<Tour>> GetToursByDestinationAsync(string destination);
    }
}
