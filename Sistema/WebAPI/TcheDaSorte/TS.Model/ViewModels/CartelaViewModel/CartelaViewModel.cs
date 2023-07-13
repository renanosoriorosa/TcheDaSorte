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
        public string Codigo { get; private set; }
        public int PremioId { get; private set; }
        public virtual Premio Premio { get; private set; }
        public int? UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public Double Preco { get; private set; }
        public bool Sorteada { get; private set; }
        public bool CompraAprovada { get; private set; }
        public int PrimeiroNumero { get; private set; }
        public int SegundoNumero { get; private set; }
        public int TerceiroNumero { get; private set; }
        public int QuartoNumero { get; private set; }
        public int QuintoNumero { get; private set; }
    }
}
