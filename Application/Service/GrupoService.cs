using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                CodigoGrupo = g.CodigoGrupo,
                IdAsignatura = g.IdAsignatura,
                IdDocente = g.IdDocente,
                Seccion = g.Seccion
            });
        }

        public async Task<IEnumerable<GruposDTO>> ListarEmpleadosPorCodigo(string codigo)
        {
            var lista = await _gruposRepository.ListarGrupoporCodigoAsync(codigo);
            return lista.Select(g => new GruposDTO
            {
                CodigoGrupo = g.CodigoGrupo,
                IdAsignatura = g.IdAsignatura,
                IdDocente = g.IdDocente,
                Seccion = g.Seccion
            });
        }

        public async Task NuevoGrupo(GruposDTO g)
        {
            var grupo = new Grupos
            {
                CodigoGrupo = g.CodigoGrupo,
                IdAsignatura = g.IdAsignatura,
                IdDocente = g.IdDocente,
                Seccion = g.Seccion
            };
            await _gruposRepository.GuardarGrupoAsync(grupo);
        }

        public async Task EditarGrupo(GruposDTO g)
        {
            var grupos = new Grupos
            {
                CodigoGrupo = g.CodigoGrupo,
                IdAsignatura = g.IdAsignatura,
                IdDocente = g.IdDocente,
                Seccion = g.Seccion
            };
            await _gruposRepository.EditarGrupoAsync(grupos);
        }

        public async Task EliminarGrupo(string Codigogrupo)
        {
            await _gruposRepository.EliminarGrupoAsync(Codigogrupo);
        }
    }
}