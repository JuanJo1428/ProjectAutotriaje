using ProjectDto.Dtos.GeneroDtos;
using System.Collections.Generic;

namespace ProjectServices.Interfaces
{
    public interface IGeneroService
    {
        List<GeneroListaDto> ObtenerGeneros();
    }
}