using GP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using TS.Models.Models.Validations;

namespace TS.Model.Models
{
    public class Premio : Entity
    {
        [Required]
        [StringLength(8)]
        public string Codigo { get; private set; }
        public DateTime DataEnvento { get; private set; }
        public PremioStatusEnum Status { get; private set; }
        public Double ValorPremio { get; private set; }
        public int PrimeiroNumero { get; private set; }
        public int SegundoNumero { get; private set; }
        public int TerceiroNumero { get; private set; }
        public int QuartoNumero { get; private set; }
        public int QuintoNumero { get; private set; }
        public int SextoNumero { get; private set; }
        public int SetimoNumero { get; private set; }
        public int OitavoNumero { get; private set; }
        public int NonoNumero { get; private set; }
        public int DecimoNumero { get; private set; }

        public ICollection<Cartela> Cartelas { get; }

        protected Premio()
        {
        }

        public Premio(string codigo, DateTime dataEnvento, double valorPremio)
        {
            Codigo = codigo;
            DataEnvento = dataEnvento;
            ValorPremio = valorPremio;
            Status = PremioStatusEnum.Criado;
        }

        public override bool EhValido()
        {
            ValidationResult = new PremioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
