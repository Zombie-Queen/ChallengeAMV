using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Application.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public string Client { get; set; }

        public DateTime ReservationDate { get; set; }

        public int TourId { get; set; }

        public TourDto Tour { get; set; }
    }
}
