using AMV.TourReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AMV.TourReservation.Web.Models
{
    public class ReservationViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Client { get; set; }

        public DateTime ReservationDate { get; set; }

        public int TourId { get; set; }

        public TourViewModel Tour { get; set; }
        public override string ShowInformation()
        {
            return "<td style=\"display:none;\" data-tourid=\""+TourId+"\"></td>\r\n" +
                   "<td>"+Client+"</td>\r\n" +
                   "<td>"+ReservationDate+"</td>";
        }
    }
}
