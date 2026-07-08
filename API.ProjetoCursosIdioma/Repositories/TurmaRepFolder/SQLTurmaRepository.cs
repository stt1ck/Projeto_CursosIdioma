using API.ProjetoCursosIdioma.Data;
using API.ProjetoCursosIdioma.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.ProjetoCursosIdioma.Repositories.TurmaRepFolder
{
    public class SQLTurmaRepository : ITurmaRepository
    {
        private readonly PCI_DbContext _dbContext;

        public SQLTurmaRepository(PCI_DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Turma> CreateAsync(Turma turma)
        {
            await _dbContext.Turmas.AddAsync(turma);
            await _dbContext.SaveChangesAsync();
            return turma;
        }

        public async Task<Turma?> DeleteAsync(Guid Id)
        {
            var existingTurma = await _dbContext.Turmas.FirstOrDefaultAsync(t => t.Id == Id);

            if (existingTurma == null)
            {
                return null;
            }

            _dbContext.Turmas.Remove(existingTurma);
            await _dbContext.SaveChangesAsync();
            return existingTurma;
        }

        public async Task<List<Turma>> GetAllAsync()
        {
            return await _dbContext.Turmas.ToListAsync();
        }

        public async Task<Turma?> GetByIdAsync(Guid Id)
        {
            return await _dbContext.Turmas.FirstOrDefaultAsync(t => t.Id == Id);
        }

        public async Task<Turma?> UpdateAsync(Guid Id, Turma turma)
        {
            var existingTurma = await _dbContext.Turmas.FirstOrDefaultAsync(t => t.Id == Id);

            if (existingTurma == null)
            {
                return null;
            }

            existingTurma.Name = turma.Name;
            existingTurma.NumeroTurma = turma.NumeroTurma;
            existingTurma.AnoLetivo = turma.AnoLetivo;

            await _dbContext.SaveChangesAsync();
            return existingTurma;
        }
    }
}
