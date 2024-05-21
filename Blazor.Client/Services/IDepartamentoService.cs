using Blazor.Shared;

namespace Blazor.Client.Services
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDTO>> Lista();
    }
}
