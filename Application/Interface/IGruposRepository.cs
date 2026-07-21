using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IGruposRepository
    {
        public Task<IEnumerable<Grupos>> ListarGrupoAsync();
        public Task<IEnumerable<Grupos>> ListarGrupoporCodigoAsync(string Codigogrupo);
        Task GuardarGrupoAsync(Grupos grupo);
        Task EditarGrupoAsync(Grupos grupo);
        Task EliminarGrupoAsync(int id);

    }
}
