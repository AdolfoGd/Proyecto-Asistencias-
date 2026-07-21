using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class SesionDeClaseService
    {
        private readonly ISeseionDeClaseRepository _sesionDeClaseRepository;

        public SesionDeClaseService(ISeseionDeClaseRepository sesionDeClaseRepository)
        {
            _sesionDeClaseRepository = sesionDeClaseRepository;
        }

        public async Task<IEnumerable<Sesion_de_ClaseDTO>> ListarSesionDeClaseAsync()
        {
           var lista = await _sesionDeClaseRepository.ListarSesionDeClaseAsync();
            return lista.Select(s => new Sesion_de_ClaseDTO
            {
                IdSesion = s.IdSesion,
                CodigoGrupo = s.CodigoGrupo,
                TemaImpartido = s.TemaImpartido,
                FechaHoraInicio = s.FechaHoraInicio,
                FechaHoraFin = s.FechaHoraFin,
                Seccion = s.Seccion,
                Departamento = s.Departamento
            });
        }
        public async Task<IEnumerable<Sesion_de_ClaseDTO>> ListarSesionDeClasePorIdGrupoAsync(int IdGrupo)
        {
            var lista = await _sesionDeClaseRepository.ListarSesionDeClasePorIdGrupoAsync(IdGrupo);
            return lista.Select(s => new Sesion_de_ClaseDTO
            {
                IdSesion = s.IdSesion,
                CodigoGrupo = s.CodigoGrupo,
                TemaImpartido = s.TemaImpartido,
                FechaHoraInicio = s.FechaHoraInicio,
                FechaHoraFin = s.FechaHoraFin,
                Seccion = s.Seccion,
                Departamento = s.Departamento
            });
        }
        public async Task NuevaSesion(Sesion_de_ClaseDTO s)
        {
            var sesion_De_Classe = new Sesion_de_Classe
            {
                CodigoGrupo = s.CodigoGrupo,
                TemaImpartido = s.TemaImpartido,
                FechaHoraInicio = s.FechaHoraInicio,
                FechaHoraFin = s.FechaHoraFin,
            };
            await _sesionDeClaseRepository.GuardarSesionDeClaseAsync(sesion_De_Classe);
        }
        public async Task EditarSesion(Sesion_de_ClaseDTO s)
        {
            var sesion = new Sesion_de_Classe
            {
                IdSesion = s.IdSesion,
                CodigoGrupo = s.CodigoGrupo,
                TemaImpartido = s.TemaImpartido,
                FechaHoraInicio = s.FechaHoraInicio,
                FechaHoraFin = s.FechaHoraFin,
            };
            await _sesionDeClaseRepository.EditarSesionDeClaseAsync(sesion);
        }
        public async Task EliminarSesion(Sesion_de_ClaseDTO s)
        {
            var sesion = new Sesion_de_Classe
            {
                IdSesion = s.IdSesion
            };
        }
    }
}
