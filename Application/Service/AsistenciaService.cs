using Application.DTOs;
using Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class AsistenciaService
    {
        private readonly IAsistenciaRepository _asistenciaRepository;

        public AsistenciaService(IAsistenciaRepository asistenciaRepository)
        {
            _asistenciaRepository = asistenciaRepository;
        }
        public async Task<IEnumerable<AsistenciaDTO>>ListarAsistencia()
        {
            var lista = await _asistenciaRepository.ListarAsistenciaAsync();
            return lista.Select(a => new AsistenciaDTO
            {
                IdAsistencia = a.IdAsistencia,
                IdSesion = a.IdSesion,
                Carnet = a.Carnet,
                Estado = a.Estado,
                Hora_Registro = a.Hora_Registro
                
            });

        }
        public async Task NuevaAsistencia(AsistenciaDTO a)
        {
            var asistencia = new Domain.Entities.Asistencia
            {
                IdSesion = a.IdSesion,
                Carnet = a.Carnet,
                Estado = a.Estado,
                Hora_Registro = a.Hora_Registro

            };
            await _asistenciaRepository.NuevaAsistenciaAsync(asistencia);
        }
        public async Task EditarAsistencia(AsistenciaDTO a)
        {
            var asistencia = new Domain.Entities.Asistencia
            {
                IdAsistencia = a.IdAsistencia,
                IdSesion = a.IdSesion,
                Carnet = a.Carnet,
                Estado = a.Estado,
            };
            await _asistenciaRepository.EditarAsistenciaAsync(asistencia);
        }
    }   
}
