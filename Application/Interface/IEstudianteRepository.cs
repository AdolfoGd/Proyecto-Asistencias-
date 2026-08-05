using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> ListarEstudiantesAsync();
        Task<IEnumerable<Estudiante>> ListarEstudiantesPorCarnetAsync(string carnet);
        Task GuardarEstudianteAsync(Estudiante estudiante);
        Task EditarEstudianteAsync(Estudiante estudiante);
        Task EliminarEstudianteAsync(string carnet);
    }
}