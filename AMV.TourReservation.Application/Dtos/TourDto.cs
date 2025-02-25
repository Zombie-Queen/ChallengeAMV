﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMV.TourReservation.Application.Dtos
{
    public class TourDto
    {

        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

    }
}
