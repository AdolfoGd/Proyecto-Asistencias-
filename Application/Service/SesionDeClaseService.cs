using Application.DTOs;
using Application.Interface;
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
    }
}
