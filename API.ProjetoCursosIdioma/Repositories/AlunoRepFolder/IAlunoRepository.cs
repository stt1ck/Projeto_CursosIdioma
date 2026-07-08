using API.ProjetoCursosIdioma.Models.Domain;

namespace API.ProjetoCursosIdioma.Repositories.AlunoRepFolder
{
    public interface IAlunoRepository //lembrar de trocar Class por Inteface
    {
        Task<List<Aluno>> GetAllAsync();

        Task<Aluno?> GetByIdAsync(Guid Id);

        Task<Aluno> CreateAsync(Aluno aluno);

        Task<Aluno?> UpdateAsync(Guid Id, Aluno aluno);

        Task<Aluno?> DeleteAsync(Guid Id);
    }
}
