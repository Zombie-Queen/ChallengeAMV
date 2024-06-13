using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMV.TourReservation.Web.Models
{
    public class TourViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public override string ShowInformation()
        {
            return "<h5 class=\"card-title\">" + Name + "</h5>\r\n " +
                    "<p class=\"card-text\">\r\n" +
                    "Destino: " + Destination + " <br />\r\n" +
                    "Precio: " + Price + " <br />\r\n" +
                    "Fecha de Inicio: " + StartDate.ToShortDateString() + " <br />\r\n" +
                    "Fecha de Fin: " + EndDate.ToShortDateString() + "\r\n" +
                    "</p>";
        }
    }
}
