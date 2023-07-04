using System.ComponentModel.DataAnnotations;
using TS.Models.Models.Validations;

namespace TS.Model.Models
{
    public class Cartela : Entity
    {
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

        protected Cartela()
        {
        }

        public void GerarCodigo()
        {
            Random numAleatorio = new Random();
            Codigo = numAleatorio.Next(99999999).ToString();
        }

        public override bool EhValido()
        {
            ValidationResult = new CartelaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
