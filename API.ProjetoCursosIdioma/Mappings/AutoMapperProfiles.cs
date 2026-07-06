using API.ProjetoCursosIdioma.Models.Domain;
using API.ProjetoCursosIdioma.Models.Dto.AlunoDto_s;
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
        }
    }
}
