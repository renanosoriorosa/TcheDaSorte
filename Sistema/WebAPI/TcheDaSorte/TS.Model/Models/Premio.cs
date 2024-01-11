using GP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using TS.Model.Interfaces;
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

        public virtual ICollection<Cartela> Cartelas { get; }

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

        //public override bool EhValido(IPremioService iPremioService)
        //{
        //    ValidationResult = new PremioValidation(iPremioService).Validate(this);
        //    return ValidationResult.IsValid;
        //}

        public void SetaParaSorteado()
        {
            Status = PremioStatusEnum.Sorteado;
        }

        public void SetarDadosSorteados(List<int> numerosSorteados)
        {
            for (int i = 0; i < numerosSorteados.Count; i++)
            {
                switch (i)
                {
                    case 0: PrimeiroNumero = numerosSorteados[i]; break;
                    case 1: SegundoNumero = numerosSorteados[i]; break;
                    case 2: TerceiroNumero = numerosSorteados[i]; break;
                    case 3: QuartoNumero = numerosSorteados[i]; break;
                    case 4: QuintoNumero = numerosSorteados[i]; break;
                    case 5: SextoNumero = numerosSorteados[i]; break;
                    case 6: SetimoNumero = numerosSorteados[i]; break;
                    case 7: OitavoNumero = numerosSorteados[i]; break;
                    case 8: NonoNumero = numerosSorteados[i]; break;
                    case 9: DecimoNumero = numerosSorteados[i]; break;
                }
            }
        }
    }
}
