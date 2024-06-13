using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Infrastructure.Persistence
{
    public class TourReservationDbContext : DbContext, IUnitOfWork
    {

        public TourReservationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected TourReservationDbContext()
        {
        }

        public virtual DbSet<Tour> Tours { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
