using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.RepositoryInterfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<List<Reservation>> GetReservationsWithTour();

        Task<List<Reservation>> GetReservationsByTour(int idtour);
    }
}
