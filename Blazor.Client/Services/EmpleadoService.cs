using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _http;

        public EmpleadoService(HttpClient http)
        {
            this._http = http;
        }


        public async Task<List<EmpleadoDTO>> GetAll()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<EmpleadoDTO>>>("/api/empleado");
            if (result!.IsSuccess)
            {
                return result.Data!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }



        public async Task<EmpleadoDTO> GetById(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<EmpleadoDTO>>($"/api/empleado/{id}");
            if (result!.IsSuccess)
            {
                return result.Data!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        public async Task<EmpleadoDTO> Create(EmpleadoDTO empleado)
        {
            var result = await _http.PostAsJsonAsync($"/api/empleado",empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<EmpleadoDTO>>();
            if (response!.IsSuccess)
            {
                return response.Data!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<bool> Delete(int Id)
        {
            var result = await _http.DeleteAsync($"/api/empleado/{Id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<bool>>();

            if (response!.IsSuccess)
            {
                return response.Data!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }




        public async Task<EmpleadoDTO> Update(int Id, EmpleadoDTO empleado)
        {
            var result = await _http.PutAsJsonAsync($"/api/empleado/{Id}", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<EmpleadoDTO>>();
            if (response!.IsSuccess)
            {
                return response.Data!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }
    }
}
