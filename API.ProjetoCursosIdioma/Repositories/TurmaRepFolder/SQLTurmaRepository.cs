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

        //CRUD

        public async Task<Turma> CreateAsync(Turma turma)
        {
            await _dbContext.Turmas.AddAsync(turma);
            await _dbContext.SaveChangesAsync();
            return turma;
        }

        public async Task<List<Turma>> GetAllAsync()
        {
            return await _dbContext.Turmas
                .Include("NivelTurma")
                .ToListAsync();
        }

        public async Task<Turma?> GetByIdAsync(Guid Id)
        {
            return await _dbContext.Turmas
                .Include("NivelTurma")
                .FirstOrDefaultAsync(t => t.Id == Id);
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
            existingTurma.NivelTurmaId = turma.NivelTurmaId;

            await _dbContext.SaveChangesAsync();
            return existingTurma;
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

        //Validation

        public async Task<bool> NivelTurmaExistAsync(Guid NivelTurmaId)
        {
            return await _dbContext.NivelTurmas.AnyAsync(n => n.Id == NivelTurmaId);
        }

        public async Task<bool> TurmaExistAsync(string Name, Guid NivelTurmaId, int AnoLetivo, string NumeroTurma)
        {
            return await _dbContext.Turmas.AnyAsync(t => t.Name == Name &&
                                                    t.NivelTurmaId == NivelTurmaId &&
                                                    t.AnoLetivo == AnoLetivo &&
                                                    t.NumeroTurma == NumeroTurma);
        }

    }
}
