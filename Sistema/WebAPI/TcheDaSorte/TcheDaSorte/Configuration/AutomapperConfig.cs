using AutoMapper;
using TS.Model.ViewModels;
using TS.Model.Models;

namespace TS.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Premio, PremioViewModel>().ReverseMap();
            CreateMap<Cartela, CartelaViewModel>().ReverseMap();
        }
    }
}
