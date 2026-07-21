using Application.DTOs;
using Application.Interface;
using Domain.Entities;
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
        public async Task<IEnumerable<AsistenciaDTO>>ListarAsistenciasPorSesion(int idsesion)
        {
            var lista = await _asistenciaRepository.ListarAsistenciaPorSesionAsync(idsesion);
            return lista.Select(s => new AsistenciaDTO
            {
                IdAsistencia = s.IdAsistencia,
                IdSesion = s.IdSesion,
                Carnet = s.Carnet,
                Estado= s.Estado,
                Hora_Registro = s.Hora_Registro
            });

        }
        public async Task NuevaAsistencia(AsistenciaDTO a)
        {
            var asistencia = new Asistencia
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
            var asistencia = new Asistencia
            {
                IdAsistencia = a.IdAsistencia,
                Estado = a.Estado,
                Hora_Registro = a.Hora_Registro
            };
            await _asistenciaRepository.EditarAsistenciaAsync(asistencia);
        }
        public async Task EliminarAsistencia(AsistenciaDTO a)
        {
            var asistencia = new Asistencia
            {
                IdAsistencia = a.IdAsistencia
            };
        }
    }   
}
