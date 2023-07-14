using GP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using TS.Model.Models;

namespace TS.Model.ViewModels
{
    public class PremioViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Codigo { get; set; }
        public DateTime DataEnvento { get; set; }
        public PremioStatusEnum Status { get; set; }
        public Double ValorPremio { get; set; }
        public int PrimeiroNumero { get; set; }
        public int SegundoNumero { get; set; }
        public int TerceiroNumero { get; set; }
        public int QuartoNumero { get; set; }
        public int QuintoNumero { get; set; }
        public int SextoNumero { get; set; }
        public int SetimoNumero { get; set; }
        public int OitavoNumero { get; set; }
        public int NonoNumero { get; set; }
        public int DecimoNumero { get; set; }

        public List<CartelaViewModel> Cartelas { get; set; }
    }
}
