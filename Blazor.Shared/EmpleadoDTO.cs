using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NombreCompleto { get; set; } = null!;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdDepartamento { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }

        public DepartamentoDTO? Departamento { get; set; } = null!;
    }
}
