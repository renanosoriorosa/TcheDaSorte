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

        public Cartela(int premioId, double preco, 
            int primeiroNumero, 
            int segundoNumero, 
            int terceiroNumero, 
            int quartoNumero, 
            int quintoNumero)
        {
            GerarCodigo();
            PremioId = premioId;
            Preco = preco;
            PrimeiroNumero = primeiroNumero;
            SegundoNumero = segundoNumero;
            TerceiroNumero = terceiroNumero;
            QuartoNumero = quartoNumero;
            QuintoNumero = quintoNumero;

            Sorteada = false;
            CompraAprovada = false;
        }

        public Cartela(int id, string codigo, int premioId, int? usuarioId, 
            double preco,
            bool sorteada, 
            bool compraAprovada, 
            int primeiroNumero, 
            int segundoNumero, 
            int terceiroNumero, 
            int quartoNumero, 
            int quintoNumero)
        {
            Id = id;
            Codigo = codigo;
            PremioId = premioId;
            UsuarioId = usuarioId;
            Preco = preco;
            Sorteada = sorteada;
            CompraAprovada = compraAprovada;
            PrimeiroNumero = primeiroNumero;
            SegundoNumero = segundoNumero;
            TerceiroNumero = terceiroNumero;
            QuartoNumero = quartoNumero;
            QuintoNumero = quintoNumero;
        }

        public void GerarCodigo()
        {
            Random numAleatorio = new Random();
            Codigo = numAleatorio.Next(99999999).ToString();
        }

        public bool DisponivelPraCompra()
        {
            if(UsuarioId == null)
                return true;

            return false;
        }

        public void ReservarCartela(int idUsuario)
        {
            this.UsuarioId = idUsuario;
        }

        public void RemoverReserva()
        {
            this.UsuarioId = null;
        }

        public override bool EhValido()
        {
            ValidationResult = new CartelaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
