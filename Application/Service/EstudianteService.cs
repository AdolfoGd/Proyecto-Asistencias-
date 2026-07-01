using Application.DTOs;
using Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class EstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;
        public EstudianteService(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }
        public async Task<IEnumerable<EstudianteDTO>> ListarEstudiantes()
        {
            var lista = await _estudianteRepository.ListarEstudiantesAsync();
            return lista.Select(e => new EstudianteDTO
            {
                Carnet = e.Carnet,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Carrera = e.Carrera,
                AñoIngreso = e.AñoIngreso,
                CantidadFaltas = e.CantidadFaltas
            });
        }
    }
}
