using AutoMapper;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Mappings
{
    public class ModelMap : Profile
    {
        public ModelMap()
        {
            CreateMap<UsuarioModel, Usuario>();
            CreateMap<NoticiaModel, Noticia>();
        }
    }
}
