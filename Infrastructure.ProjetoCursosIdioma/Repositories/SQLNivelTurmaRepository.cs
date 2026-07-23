using Domain.ProjetoCursosIdioma.Entities;
using Domain.ProjetoCursosIdioma.Repositories;
using Infrastructure.ProjetoCursosIdioma.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ProjetoCursosIdioma.Repositories
{
    public class SQLNivelTurmaRepository : INivelTurmaRepository
    {
        private readonly PCI_DbContext _DbContext;

        public SQLNivelTurmaRepository(PCI_DbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        public async Task<List<NivelTurma>> GetAllAsync()
        {
            return await _DbContext.NivelTurmas.AsNoTracking().ToListAsync();
        }
    }
}
