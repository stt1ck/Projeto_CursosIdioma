using Domain.ProjetoCursosIdioma.Entities;

namespace Domain.ProjetoCursosIdioma.Repositories
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
            string NumeroTurma,
            Guid? IdIgnorado = null
            );

        //Relacionamentos
        Task<List<Turma>> GetByIdsAsync(List<Guid> turmaIds);
        Task<int> CountAlunosAsync(Guid turmaId);
    }
}
