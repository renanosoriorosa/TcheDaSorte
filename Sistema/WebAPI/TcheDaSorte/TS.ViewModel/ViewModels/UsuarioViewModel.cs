using System.ComponentModel.DataAnnotations;

namespace TS.ViewModels.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Nome { get; set; }
        public Guid IndentityId { get; set; }
        public bool Ativo { get; set; }
    }
}
