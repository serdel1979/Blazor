using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly HttpClient _http;

        public DepartamentoService(HttpClient http)
        {
            this._http = http;
        }
        public async Task<List<DepartamentoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<DepartamentoDTO>>>("api/departamento");
            if(result!.IsSuccess)
            {
                return result.Data!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
    }
}
