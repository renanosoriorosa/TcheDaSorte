using AutoMapper;
using TS.ViewModels.ViewModels;
using TS.Model.Models;

namespace TS.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        }
    }
}
