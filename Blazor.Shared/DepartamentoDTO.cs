using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class DepartamentoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public virtual ICollection<EmpleadoDTO> Empleados { get; } = new List<EmpleadoDTO>();
    }
}
