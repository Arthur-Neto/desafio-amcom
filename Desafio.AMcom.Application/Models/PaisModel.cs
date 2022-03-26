using AutoMapper;
using Desafio.AMcom.Domain;

namespace Desafio.AMcom.Application.Models
{
    public class PaisModel
    {
        public string Gentilico { get; set; }
        public string NomePais { get; set; }
        public string NomePaisInt { get; set; }
        public string Sigla { get; set; }
    }

    public class PaisModelMapping : Profile
    {
        public PaisModelMapping()
        {
            CreateMap<Pais, PaisModel>()
                .ForMember(m => m.Gentilico, opts => opts.MapFrom(src => src.Gentilico))
                .ForMember(m => m.Sigla, opts => opts.MapFrom(src => src.Sigla))
                .ForMember(m => m.NomePaisInt, opts => opts.MapFrom(src => src.NomePaisInt))
                .ForMember(m => m.NomePais, opts => opts.MapFrom(src => src.NomePais));
        }
    }
}
