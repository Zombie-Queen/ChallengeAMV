using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Application.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public bool CheckCredentials(User user);
    }
}
