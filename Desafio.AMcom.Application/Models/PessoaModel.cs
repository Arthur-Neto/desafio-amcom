using AutoMapper;
using Desafio.AMcom.Domain;

namespace Desafio.AMcom.Application.Models
{
    public class PessoaModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Nome { get; set; }
    }

    public class PessoaModelMapping : Profile
    {
        public PessoaModelMapping()
        {
            CreateMap<Pessoa, PessoaModel>()
                .ForMember(m => m.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(m => m.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(m => m.Avatar, opts => opts.MapFrom(src => src.Avatar))
                .ForMember(m => m.Nome, opts => opts.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
