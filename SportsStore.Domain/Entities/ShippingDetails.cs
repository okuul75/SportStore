﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Prosze podać nazwisko")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prosze podać poprawny adres")]
        [Display(Name = "Wiersz 1")]
        public string Line1 { get; set; }

        [Display(Name = "Wiersz 2")]
        public string Line2 { get; set; }

        [Display(Name = "Wiersz 3")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Prosze podać nazwę miasta")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Prosze podać nazwę województwa")]
        [Display(Name = "Województwo")]
        public string State { get; set; }

        [Required(ErrorMessage = "Prosze podać kod pocztowy")]
        [Display(Name = "Kod pocztowy")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Prosze podać nazwę kraju")]
        [Display(Name = "Kraj")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
