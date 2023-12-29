using GP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using TS.Model.Models;

namespace TS.Model.ViewModels
{
    public class CartelaCompraViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int PremioId { get; set; }
        public int UsuarioId { get; set; }

        public CartelaCompraViewModel()
        {
        }

        public CartelaCompraViewModel(int id, string codigo, int premioId, int usuarioId)
        {
            Id = id;
            Codigo = codigo;
            PremioId = premioId;
            UsuarioId = usuarioId;
        }
    }
}
