using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IAsistenciaRepository
    {
        Task<IEnumerable<Asistencia>> ListarAsistenciaAsync();
        Task<IEnumerable<Asistencia>> ListarAsistenciaPorSesionAsync(int idsesion);
        Task NuevaAsistenciaAsync(Asistencia asistencia);
        Task EditarAsistenciaAsync(Asistencia asistencia);
        Task EliminarAsistenciaAsync(int id);
      
    }
}
