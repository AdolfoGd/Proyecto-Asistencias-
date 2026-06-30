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
                
            });

        }
}
}
