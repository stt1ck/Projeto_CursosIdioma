using API.ProjetoCursosIdioma.Models.Domain;
using API.ProjetoCursosIdioma.Models.Dto.AlunoDto_s;
using API.ProjetoCursosIdioma.Models.Dto.NivelTurmaDto_s;
using API.ProjetoCursosIdioma.Models.Dto.TurmaDto;
using AutoMapper;

namespace API.ProjetoCursosIdioma.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Auto Mapping Aluno
            CreateMap<Aluno, AlunoDto>().ReverseMap();
            CreateMap<Aluno, AlunoAddRequestDto>().ReverseMap();
            CreateMap<Aluno, AlunoUpdateRequestDto>().ReverseMap();

            //Auto Mapping Turma
            CreateMap<Turma, TurmaDto>().ReverseMap();
            CreateMap<Turma, TurmaAddRequestDto>().ReverseMap();
            CreateMap<Turma, TurmaUpdateRequestDto>().ReverseMap();
            CreateMap<Turma, TurmaDto>()
                .ForMember(dest => dest.Nivel,
                    opt => opt.MapFrom(src => src.NivelTurma.Name));

            //Auto Mapping NivelTurma
            CreateMap<NivelTurma, NivelTurmaDto>().ReverseMap();
        }
    }
}
