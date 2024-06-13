using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Domain.Entities
{
    public class Tour : BaseEntity
    {
        public string Name { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        #region Navigation Properties

        public virtual List<Reservation> Reservations { get; set; }

        #endregion
    }
}
