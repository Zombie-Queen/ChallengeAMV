using AMV.TourReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Infrastructure.Persistence.Configuration
{
    public class TourEntityTypeConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tour");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(t => t.Destination)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired();

            builder.Property(t => t.Price)
                .IsRequired();

            builder
                .HasMany(t => t.Reservations)
                .WithOne(r => r.Tour)
                .HasForeignKey(r => r.TourId);
            
        }
    }
}
