using Application.ProjetoCursosIdioma.Common;
using Application.ProjetoCursosIdioma.Dto.TurmaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Interfaces
{
    public interface ITurmaService
    {
        Task<List<TurmaDto>> GetAllAsync();
        Task<Resultado<TurmaDto>> GetByIdAsync(Guid id);
        Task<Resultado<TurmaDto>> CreateAsync(TurmaAddRequestDto turmaAddRequestDto);
        Task<Resultado<TurmaDto>> UpdateAsync(Guid Id, TurmaUpdateRequestDto turmaUpdateRequestDto);
        Task<Resultado<TurmaDto>> DeleteAsync(Guid Id);
    }
}
