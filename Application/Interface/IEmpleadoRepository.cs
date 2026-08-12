using Domain.Entities;

namespace Application.Interface
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> ListarEmpleadosAsync();
        Task<IEnumerable<Empleado>> ListarEmpleadosPorDepartamentoAsync(string departamento);
        Task GuardarEmpleadoAsync(Empleado empleado);
        Task EditarEmpleadoAsync(Empleado empleado);
        Task EliminarEmpleadoAsync(string codigoEmpleado);
        Task<Empleado?> ValidarCredencialesAsync(string codigo, string contraseña);
    }
}