using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Matriculas
    {
        public int IdMatricula {  get; set; }
        [Display(Name = "Ingrese su Id")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdEstudiante { get; set; }
        [Display(Name = "Ingrese su codigo de grupo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CodidoGrupo { get; set; }
        [Display(Name = "Ingrese la fecha de registro")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateOnly FechaRegistro { get; set; }
    }
}
