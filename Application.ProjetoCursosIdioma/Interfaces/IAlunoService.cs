using Application.ProjetoCursosIdioma.Common;
using Application.ProjetoCursosIdioma.Dto.AlunoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Interfaces
{
    public interface IAlunoService
    {
        Task<List<AlunoDto>> GetAllAsync();
        Task<Resultado<AlunoDto>> GetByIdAsync(Guid id);
        Task<Resultado<AlunoDto>> CreateAsync(AlunoAddRequestDto request);
        Task<Resultado<AlunoDto>> AddTurmaAsync(Guid alunoId, Guid turmaId);
        Task<Resultado<AlunoDto>> UpdateAsync(Guid id, AlunoUpdateRequestDto request);
        Task<Resultado<AlunoDto>> DeleteAsync(Guid id);
        Task<Resultado<AlunoDto>> RemoveTurmaAsync(Guid alunoId, Guid turmaId);
    }
}
