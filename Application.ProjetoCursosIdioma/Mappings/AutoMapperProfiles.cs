using Domain.ProjetoCursosIdioma.Entities;
using Application.ProjetoCursosIdioma.Dto.AlunoDtos;
using Application.ProjetoCursosIdioma.Dto.NivelTurmaDtos;
using Application.ProjetoCursosIdioma.Dto.TurmaDtos;
using AutoMapper;

namespace Application.ProjetoCursosIdioma.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Auto Mapping Aluno
            CreateMap<AlunoTurma, TurmaDoAlunoDto>()
                .ForMember(
                    destination => destination.Name,
                    opt => opt.MapFrom(
                    source => source.Turma.Name
                    )
                )
                .ForMember(
                    destination => destination.Nivel,
                    opt => opt.MapFrom(
                    source => source.Turma.NivelTurma.Name
                    )
                )
                .ForMember(
                    destination => destination.AnoLetivo,
                    opt => opt.MapFrom(
                    source => source.Turma.AnoLetivo
                    )
                )
                .ForMember(
                    destination => destination.NumeroTurma,
                    opt => opt.MapFrom(
                    source => source.Turma.NumeroTurma
                    )
                )
                .ForMember(
                    destination => destination.TurmaId,
                    opt => opt.MapFrom(
                    source => source.TurmaId
                    )
                );

            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    destination => destination.Turmas,
                    opt => opt.MapFrom(
                        source => source.AlunoTurmas
                    )
                );
            CreateMap<AlunoAddRequestDto, Aluno>()
                .ForMember(destination => destination.AlunoTurmas,
                    opt => opt.Ignore());
            CreateMap<Aluno, AlunoUpdateRequestDto>().ReverseMap();

            //Auto Mapping Turma
            CreateMap<Turma, TurmaAddRequestDto>().ReverseMap();
            CreateMap<Turma, TurmaUpdateRequestDto>().ReverseMap();
            CreateMap<Turma, TurmaDto>()
                .ForMember(
                    destination => destination.Nivel,
                    opt => opt.MapFrom(
                    source => source.NivelTurma.Name
                    )
                );

            //Auto Mapping NivelTurma
            CreateMap<NivelTurma, NivelTurmaDto>().ReverseMap();
        }
    }
}
