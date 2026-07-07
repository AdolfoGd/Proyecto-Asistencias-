using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class SesionDeClaseRepository : ISeseionDeClaseRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public async Task<IEnumerable<Sesion_de_Classe>> ListarSesionDeClaseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Sesion_de_Classe>> ListarSesionDeClasePorIdGrupoAsync(int IdGrupo)
        {
            throw new NotImplementedException();
        }

        public async Task GuardarSesionDeClaseAsync(Sesion_de_Classe sesionDeClase)
        {
            throw new NotImplementedException();
        }

        public async Task EditarSesionDeClaseAsync(Sesion_de_Classe sesionDeClase)
        {
            throw new NotImplementedException();
        }

        public async Task EliminarSesionDeClaseAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
