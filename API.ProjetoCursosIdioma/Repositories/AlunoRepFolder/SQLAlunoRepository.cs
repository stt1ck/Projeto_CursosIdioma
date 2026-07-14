using API.ProjetoCursosIdioma.Data;
using API.ProjetoCursosIdioma.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.ProjetoCursosIdioma.Repositories.AlunoRepFolder
{
    public class SQLAlunoRepository : IAlunoRepository
    {
        private readonly PCI_DbContext _DbContext;

        public SQLAlunoRepository(PCI_DbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        //CRUD

        public async Task<Aluno> CreateAsync(Aluno aluno)
        {
            await _DbContext.Alunos.AddAsync(aluno);
            await _DbContext.SaveChangesAsync();
            return aluno;
        }

        public async Task<List<Aluno>> GetAllAsync()
        {
            return await _DbContext.Alunos.ToListAsync();
        }

        public async Task<Aluno?> GetByIdAsync(Guid Id)
        {
            return await _DbContext.Alunos.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<Aluno?> UpdateAsync(Guid Id, Aluno aluno)
        {
            var existingAluno = await _DbContext.Alunos.FirstOrDefaultAsync(a => a.Id == Id);
            if (existingAluno == null) { return null; }

            existingAluno.Nome = aluno.Nome;
            existingAluno.Email = aluno.Email;

            await _DbContext.SaveChangesAsync();
            return existingAluno;
        }

        public async Task<Aluno?> DeleteAsync(Guid Id)
        {
            var existingAluno = await _DbContext.Alunos.FirstOrDefaultAsync(a => a.Id == Id);

            if (existingAluno == null) { return null; }

            _DbContext.Alunos.Remove(existingAluno);
            await _DbContext.SaveChangesAsync();
            return existingAluno;
        }

        //Validation

        public async Task<bool> EmailAlreadyUsedAsync(string email)
        {
            return await _DbContext.Alunos.AnyAsync(a => a.Email == email);
        }

        public async Task<bool> CPFAlreadyUsedAsync(string cpf)
        {
            return await _DbContext.Alunos.AnyAsync(a => a.Cpf == cpf);
        }
    }
}
