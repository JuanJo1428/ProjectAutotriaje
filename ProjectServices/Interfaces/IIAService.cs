using System.Threading.Tasks;

namespace ProjectServices.Interfaces
{
    public interface IIAService
    {
        Task<string> GenerarRespuestaAsync(string prompt);
    }
}
