using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class EmpleadoDTO
    {
        public int CodigoEmpleado { get; set; }
        [StringLength(15)]
        [Display(Name = "Ingrese la contraseña")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Contraseña { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese su primer nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? PrimerNombre { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese su segundo nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? SegundoNombre { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese su primer apellido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? PrimerApellido { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese su segundo apellido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? SegundoApellido { get; set; }
        [StringLength(30)]
        [Display(Name = "Ingrese su departamento")]
        [Required(ErrorMessage = "Este campo es obligatorio")]]
        public string? Departamento { get; set; }
    }
}
