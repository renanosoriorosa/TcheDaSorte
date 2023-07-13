using System.ComponentModel.DataAnnotations;

namespace TS.Model.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Nome { get; set; }
        public string IndentityId { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }

    }
}
