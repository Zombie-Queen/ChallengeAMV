using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; }

        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task DeleteAsync(int id);
    }
}
