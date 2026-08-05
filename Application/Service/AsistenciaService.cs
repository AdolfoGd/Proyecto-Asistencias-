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
        public async Task EditarAsistencia(int idAsistencia, string estado )
        {
            var asistencia = new Asistencia
            {
                IdAsistencia = idAsistencia,
                Estado = estado
            };
            await _asistenciaRepository.EditarAsistenciaAsync(asistencia, estado);
        }
        public async Task EliminarAsistencia(int idAsistencia)
        {
            await _asistenciaRepository.EliminarAsistenciaAsync(idAsistencia);
        }

    }   
}
