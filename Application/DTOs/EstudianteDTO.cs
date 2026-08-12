using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class EstudianteDTO
    {
        public string? Carnet { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese la contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string? Contraseña { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese su primer nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? PrimerNombre { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese su segundo nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
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
        [Display(Name = "Ingrese su carrera")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Carrera { get; set; }
        [Display(Name = "Ingrese el año de ingreso ")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int AnioIngreso { get; set; }
        [Display(Name = "Ingrese la cantidad de faltas")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CantidadFaltas { get; set; }
    }
}
