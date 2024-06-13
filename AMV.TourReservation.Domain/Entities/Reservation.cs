
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public string Client { get; set; }

        public DateTime ReservationDate { get; set; }

        public int TourId { get; set; }

        public Tour Tour { get; set; }

    }
}
