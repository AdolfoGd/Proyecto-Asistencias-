using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class EmpladoRepository : IEmpleadoRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        Task<IEnumerable<Empleado>> IEmpleadoRepository.ListarEmpleadosAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Empleado>> ListarEmpleadosPorDepartamentoAsync(string departamento)
        {
            throw new NotImplementedException();
        }
        public Task NuevoEmpleado(Empleado empleado)
        {
            throw new NotSupportedException();
        }
        public Task GuardarEmpleadoAsync(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public Task EditarEmpleadoAsync(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public Task EliminarEmpleadoAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
