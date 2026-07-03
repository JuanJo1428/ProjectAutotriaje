using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos.GeneroDtos;
using ProjectServices.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectServices.Implementations
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public List<GeneroListaDto> ObtenerGeneros()
        {
            return _generoRepository.ObtenerTodos().Select(g => new GeneroListaDto
                {
                    IdGenero = g.IdGenero,
                    Descripcion = g.Descripcion
                })
                .ToList();
        }
    }
}