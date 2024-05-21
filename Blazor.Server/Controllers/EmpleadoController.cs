using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbcrudBlazorContext _context;

        public EmpleadoController(DbcrudBlazorContext context)
        {
            this._context = context;
        }




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResponseApi<List<EmpleadoDTO>> response = new ResponseApi<List<EmpleadoDTO>>();
            response.Data = new List<EmpleadoDTO>();
            try
            {
                foreach (var item in await _context.Empleados.Include(d=>d.IdDepartamentoNavigation)
                    .ToListAsync())
                {
                    response.Data.Add(new EmpleadoDTO
                    {
                        Id = item.Id,
                        NombreCompleto = item.NombreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDTO
                        {
                            Id = item.IdDepartamentoNavigation.Id,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }


        [HttpGet("Id:int")]
        public async Task<IActionResult> GetEmpleado(int Id)
        {
            ResponseApi<EmpleadoDTO> response = new ResponseApi<EmpleadoDTO>();
            response.Data = new EmpleadoDTO();
            try
            {
                var entityEmpleado = await _context.Empleados.Include(e=>e.IdDepartamentoNavigation).FirstOrDefaultAsync(e => e.Id == Id);
                if (entityEmpleado == null)
                {
                    throw new KeyNotFoundException();
                }
                response.Data.NombreCompleto = entityEmpleado.NombreCompleto;
                response.Data.IdDepartamento = entityEmpleado.IdDepartamento;
                response.Data.Sueldo = entityEmpleado.Sueldo;
                response.Data.FechaContrato = entityEmpleado.FechaContrato;
                response.Data.Departamento = new DepartamentoDTO
                {
                    Id = entityEmpleado.IdDepartamentoNavigation.Id,
                    Nombre = entityEmpleado.IdDepartamentoNavigation.Nombre
                };

                response.IsSuccess = true;
                return Ok(response);
            }
            catch(KeyNotFoundException err)
            {
                response.IsSuccess = false;
                response.Message = err.Message;
                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }




        [HttpPost]
        public async Task<IActionResult> CreaEmpleado([FromBody] EmpleadoDTO empleado)
        {
            ResponseApi<EmpleadoDTO> response = new ResponseApi<EmpleadoDTO>();
            response.Data = new EmpleadoDTO();
            try
            {
                Empleado empleadoDb = new Empleado
                {
                    NombreCompleto = empleado.NombreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato
                };
                _context.Empleados.Add(empleadoDb);

                await _context.SaveChangesAsync();
        
                response.IsSuccess = true;
                response.Data = empleado;
                response.Message = "Empleado creado";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }



        [HttpDelete("Id:int")]
        public async Task<IActionResult> DeleteEmpleado(int Id)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();

            try
            {
                var delet = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == Id);
                if(delet == null)
                {
                    throw new KeyNotFoundException($"No existe el empleado con id {Id}");
                }
                _context.Empleados.Remove(delet);

                await _context.SaveChangesAsync();
                response.Message = $"Empleado con Id {Id} borrado!!!";
                response.IsSuccess = true;
                response.Data = true;
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Data = false;
                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Data = false;
                return StatusCode(500, response);
            }
        }



        [HttpPut("Id:int")]
        public async Task<IActionResult> UpdateEmpleado(int Id, EmpleadoDTO empleado)
        {
            ResponseApi<EmpleadoDTO> response = new ResponseApi<EmpleadoDTO>();
            response.Data = new EmpleadoDTO();
            try
            {
                var update = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == Id);
                if (update == null)
                {
                    throw new KeyNotFoundException($"No existe el empleado con id {Id}");
                }

                update.Sueldo = empleado.Sueldo;
                update.NombreCompleto = empleado.NombreCompleto;
                update.IdDepartamento = empleado.IdDepartamento;
                update.FechaContrato = empleado.FechaContrato;
                _context.Empleados.Update(update);

                await _context.SaveChangesAsync();
                response.Message = $"Empleado con Id {Id} actualizado!!!";
                response.IsSuccess = true;
                response.Data = empleado;
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(404, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error extraño";
                return StatusCode(500, response);
            }
        }

    }
}
