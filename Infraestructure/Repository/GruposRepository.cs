using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class GruposRepository : IGruposRepository
    {

        private readonly DBConnectionFactory _connectionFactory; 

        public Task<IEnumerable<Grupos>> ListarGrupoAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Grupos>> ListarGrupoporCodigoAsync(int Codigogrupo)
        {
            throw new NotImplementedException();
        }
        public Task GuardarGrupoAsync(Grupos grupo)
        {
            throw new NotImplementedException();
        }
        public Task EditarGrupoAsync(Grupos grupo)
        {
            throw new NotImplementedException();
        }
        public Task EliminarGrupoAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
