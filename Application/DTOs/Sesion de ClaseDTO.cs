using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class Sesion_de_ClaseDTO
    {
        public int IdSesion { get; set; }
        [Display(Name = "Ingrese su codido de grupo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CodigoGrupo { get; set; }
        [StringLength(25)]
        [Display(Name = "Ingrese el tema impartido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? TemaImpartido { get; set; }
        [Display(Name = "Ingrese la fecha y hora de inicio")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaHoraInicio { get; set; }
        [Display(Name = "Ingrese la fecha y hora de finalizacion")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaHoraFin { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese la seccion")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Seccion { get; set; }
        [StringLength(25)]
        [Display(Name = "Ingrese su departamento")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Departamento { get; set; }
    }
}
