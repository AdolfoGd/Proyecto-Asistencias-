using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface ISeseionDeClaseRepository
    {
        public Task<IEnumerable<Sesion_de_Classe>> ListarSesionDeClaseAsync();
        public Task<IEnumerable<Sesion_de_Classe>> ListarSesionDeClasePorIdGrupoAsync(int IdGrupo);
        Task GuardarSesionDeClaseAsync(Sesion_de_Classe sesionDeClase);
        Task EditarSesionDeClaseAsync(Sesion_de_Classe sesionDeClase);
        Task EliminarSesionDeClaseAsync(int id);
    }
}
