using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMV.TourReservation.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>()
        {
            new User(){ Username= "admin@user.com", Password="admin"},
            new User(){ Username= "user@user.com", Password="user"}
        };
        public bool CheckCredentials(User user)
        {
            return _users.Exists(x => x.Username == user.Username && x.Password == user.Password);
        }
    }
}
