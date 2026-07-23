using Application.ProjetoCursosIdioma.Dto.NivelTurmaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Interfaces
{
    public interface INivelTurmaService
    {
        Task<List<NivelTurmaDto>> GetAllAsync();
    }
}
