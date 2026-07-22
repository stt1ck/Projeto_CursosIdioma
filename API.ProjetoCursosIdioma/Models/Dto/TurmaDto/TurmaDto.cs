using API.ProjetoCursosIdioma.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace API.ProjetoCursosIdioma.Models.Dto.TurmaDto
{
    public class TurmaDto
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }
        public string NumeroTurma { get; set; }
        public int AnoLetivo { get; set; }
        public string Nivel { get; set; }

    }
}
