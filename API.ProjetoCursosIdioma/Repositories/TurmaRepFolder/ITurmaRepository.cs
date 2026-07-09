using API.ProjetoCursosIdioma.Models.Domain;

namespace API.ProjetoCursosIdioma.Repositories.TurmaRepFolder
{
    public interface ITurmaRepository
    {
        //CRUD
        Task<List<Turma>> GetAllAsync();

        Task<Turma?> GetByIdAsync(Guid id);

        Task<Turma> CreateAsync(Turma turma);

        Task<Turma?> UpdateAsync(Guid Id, Turma turma);

        Task<Turma?> DeleteAsync(Guid Id);

        //Validações
        Task<bool> NivelTurmaExistAsync(Guid NivelTurmaId);
        Task<bool> TurmaExistAsync(
            string Name,
            Guid NivelTurmaId,
            int AnoLetivo,
            string NumeroTurma
            );
    }
}
