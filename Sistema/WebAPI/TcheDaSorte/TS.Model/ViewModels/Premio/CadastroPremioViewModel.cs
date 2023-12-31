﻿using System.ComponentModel.DataAnnotations;

namespace TS.Model.ViewModels
{
    public class CadastroPremioViewModel
    {
        [Required]
        [StringLength(8)]
        public string Codigo { get; set; }
        public DateTime DataEvento { get; set; }
        public double ValorPremio { get; set; }
        public int NumeroCartelas { get; set; }
        public double PrecoCartela { get; set; }
    }
}
