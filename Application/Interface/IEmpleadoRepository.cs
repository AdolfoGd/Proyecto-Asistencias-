using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> ListarEmpleadosAsync();
        Task<IEnumerable<Empleado>> ListarEmpleadosPorDepartamentoAsync(string departamento);
        Task GuardarEmpleadoAsync(Empleado empleado);
        Task EditarEmpleadoAsync(Empleado empleado);
        Task EliminarEmpleadoAsync(int id);
    }
}
