using API.ProjetoCursosIdioma.Models.Domain;

namespace API.ProjetoCursosIdioma.Repositories.AlunoRepFolder
{
    public interface IAlunoRepository //lembrar de trocar Class por Inteface
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

        Task<bool> EmailAlreadyUsedAsync(string email);

        Task<bool> EmailAlreadyUsedByAnotherAlunoAsync(string email, Guid alunoId);

        Task<bool> CPFAlreadyUsedAsync(string cpf);


    }
}
