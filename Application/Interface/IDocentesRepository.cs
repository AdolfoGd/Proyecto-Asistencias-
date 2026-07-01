using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IDocentesRepository
    {
        Task<IEnumerable<Docentes>> ListarDocentesAsync();
        Task<IEnumerable<Docentes>> ListarDocentesPorDepartamentoAsync(string departamento);
        Task GuardarDocentesAsync(Docentes docentes);
        Task EditarDocentesAsync(Docentes docentes);
        Task EliminarDocentesAsync(int id);


    }
}
