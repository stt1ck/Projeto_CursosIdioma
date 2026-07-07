using API.ProjetoCursosIdioma.Models.Domain;

namespace API.ProjetoCursosIdioma.Repositories.TurmaRepFolder
{
    public interface ITurmaRepository
    {
        Task<List<Turma>> GetAllAsync();

        Task<Turma?> GetByIdAsync(Guid id);

        Task<Turma> CreateAsync(Turma turma);

        Task<Turma?> UpdateAsync(Guid Id, Turma turma);
        //Identificador //Propriedades do Domain Model que queremos atualizar
        Task<Turma?> DeleteAsync(Guid Id);
    }
}
