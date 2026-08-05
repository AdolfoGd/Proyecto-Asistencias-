using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq; // Necesario para .Select()
using System.Threading.Tasks; // Necesario para Task

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

        // Corregido nombre del método
        public async Task<IEnumerable<EstudianteDTO>> ListarEstudiantesPorCarnet(string carnet)
        {
            var lista = await _estudianteRepository.ListarEstudiantesPorCarnetAsync(carnet);
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

        public async Task NuevoEstudiante(EstudianteDTO e)
        {
            var estudiante = new Estudiante
            {
                Carnet = e.Carnet,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Carrera = e.Carrera,
                AñoIngreso = e.AñoIngreso
            };
            await _estudianteRepository.GuardarEstudianteAsync(estudiante);
        }

        public async Task EditarEstudiante(EstudianteDTO e)
        {
            var estudiante = new Estudiante
            {
                Carnet = e.Carnet,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Carrera = e.Carrera,
                CantidadFaltas = e.CantidadFaltas
            };
            await _estudianteRepository.EditarEstudianteAsync(estudiante);
        }
        public async Task EliminarEstudiante(string carnet)
        {
            await _estudianteRepository.EliminarEstudianteAsync(carnet);
        }
    }
}