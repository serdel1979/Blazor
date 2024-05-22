using Azure;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DbcrudBlazorContext _context;

        public DepartamentoController(DbcrudBlazorContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResponseApi<List<DepartamentoDTO>> response = new ResponseApi<List<DepartamentoDTO>>();
            response.Data = new List<DepartamentoDTO>();
            try
            {
                foreach (var item in await _context.Departamentos.ToListAsync())
                {
                    response.Data.Add(new DepartamentoDTO { 
                        Id = item.Id,
                        Nombre = item.Nombre,
                    });
                }
                response.IsSuccess = true;
                response.Message = "Listado";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
