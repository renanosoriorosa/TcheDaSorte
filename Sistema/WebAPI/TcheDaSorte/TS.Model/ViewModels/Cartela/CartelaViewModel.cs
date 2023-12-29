using GP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using TS.Model.Models;

namespace TS.Model.ViewModels
{
    public class CartelaViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Codigo { get; set; }
        public int PremioId { get; set; }
        public virtual Premio Premio { get; set; }
        public int? UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public Double Preco { get; set; }
        public bool Sorteada { get; set; }
        public bool CompraAprovada { get; set; }
        public int PrimeiroNumero { get; set; }
        public int SegundoNumero { get; set; }
        public int TerceiroNumero { get; set; }
        public int QuartoNumero { get; set; }
        public int QuintoNumero { get; set; }
    }
}
