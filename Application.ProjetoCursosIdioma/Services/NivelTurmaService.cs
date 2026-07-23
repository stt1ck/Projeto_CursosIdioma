using Application.ProjetoCursosIdioma.Dto.NivelTurmaDtos;
using Application.ProjetoCursosIdioma.Interfaces;
using AutoMapper;
using Domain.ProjetoCursosIdioma.Repositories;

namespace Application.ProjetoCursosIdioma.Services
{
    public class NivelTurmaService : INivelTurmaService
    {
        private readonly INivelTurmaRepository _repository;
        private readonly IMapper _mapper;

        public NivelTurmaService(INivelTurmaRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<List<NivelTurmaDto>> GetAllAsync()
        {
            var niveris = await _repository.GetAllAsync();

            return _mapper.Map<List<NivelTurmaDto>>(niveris);
        }
    }
}
