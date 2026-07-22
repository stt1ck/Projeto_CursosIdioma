using Domain.ProjetoCursosIdioma.Entities;

namespace Domain.ProjetoCursosIdioma.Repositories
{
    public interface IAlunoRepository
    {
        //CRUD
        Task<List<Aluno>> GetAllAsync();
        Task<Aluno?> GetByIdAsync(Guid Id);
        Task<Aluno> CreateAsync(Aluno aluno);
        Task<bool> AddTurmaAsync(Guid alunoId, Guid turmaId);
        Task<Aluno?> UpdateAsync(Guid Id, Aluno aluno);
        Task<Aluno?> DeleteAsync(Guid Id);
        Task<bool> DeleteTurmaAsync(Guid alunoId, Guid turmaId);

        //Validation
        Task<bool> EmailAlreadyUsedAsync(string email, Guid? alunoIdIgnorado = null);
        Task<bool> EmailAlreadyUsedByAnotherAlunoAsync(string email, Guid alunoId);
        Task<bool> CPFAlreadyUsedAsync(string cpf, Guid? alunoIdIgnorado = null);
    }
}
