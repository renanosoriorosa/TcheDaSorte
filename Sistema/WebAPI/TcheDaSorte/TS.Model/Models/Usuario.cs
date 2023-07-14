using System.ComponentModel.DataAnnotations;
using TS.Models.Models.Validations;

namespace TS.Model.Models
{
    public class Usuario : Entity
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Nome { get; private set; }
        public string IndentityId { get; private set; }
        public bool Ativo { get; private set; }

        public virtual ICollection<Cartela> Cartelas { get; }

        protected Usuario()
        {
        }

        public Usuario(string nome, string indentityId)
        {
            Nome = nome;
            IndentityId = indentityId;
            Ativo = true;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new UsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
