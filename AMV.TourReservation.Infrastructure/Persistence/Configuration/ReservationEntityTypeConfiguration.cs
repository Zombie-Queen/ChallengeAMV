using AMV.TourReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Infrastructure.Persistence.Configuration
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservation");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd(); 

            builder.Property(t => t.Client)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.ReservationDate)
                .IsRequired();

            builder.Property(t => t.TourId)
                .IsRequired();


        }
    }
}
