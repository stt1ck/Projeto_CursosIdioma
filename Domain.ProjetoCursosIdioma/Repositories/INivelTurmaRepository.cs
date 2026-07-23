using Domain.ProjetoCursosIdioma.Entities;

namespace Domain.ProjetoCursosIdioma.Repositories
{
    public interface INivelTurmaRepository
    {
        Task<List<NivelTurma>> GetAllAsync();
    }
}
