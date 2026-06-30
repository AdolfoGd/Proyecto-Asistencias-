using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IAsistenciaRepository
    {
        Task<IEnumerable<Asistencia>> ListarAsistenciaAsync();
        Task NuevaAsistenciaAsync(Asistencia asistencia);
        Task EditarAsistenciaAsync(Asistencia asistencia);
        Task EliminarAsistenciaAsync(Asistencia asistencia);
    }
}
