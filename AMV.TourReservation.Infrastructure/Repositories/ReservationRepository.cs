using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Domain.Entities;
using AMV.TourReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(TourReservationDbContext context) : base(context)
        {
        }

        public async Task<List<Reservation>> GetReservationsWithTour()
        {
            return await _context.Reservations.Include(x => x.Tour).ToListAsync();
        }

        public async Task<List<Reservation>> GetReservationsByTour(int idtour)
        {
            return await _context.Reservations.Include(x => x.Tour).Where(x => x.TourId == idtour).ToListAsync();
        }
    }
}
