using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class GrupoService
    {
        private readonly IGruposRepository _gruposRepository;

        public GrupoService(IGruposRepository gruposRepository)
        {
            _gruposRepository = gruposRepository;
        }

        public async Task<IEnumerable<GruposDTO>> ListarGrupoAsync()
        {
            var lista = await _gruposRepository.ListarGrupoAsync();
            return lista.Select(g => new GruposDTO
            {
                CodidoGrupo = g.CodidoGrupo,
                IdAsignatura = g.IdAsignatura,
                IdDocente = g.IdDocente,
                Seccion = g.Seccion
            });
        }
    }
}
