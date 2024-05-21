using Blazor.Shared;

namespace Blazor.Client.Services
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> GetAll();
        Task<EmpleadoDTO> GetById(int id);
        Task<EmpleadoDTO> Update(int Id, EmpleadoDTO empleado);
        Task<EmpleadoDTO> Create(EmpleadoDTO empleado);
        Task<bool> Delete(int Id);
    }
}
