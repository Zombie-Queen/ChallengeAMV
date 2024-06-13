using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.RepositoryInterfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
