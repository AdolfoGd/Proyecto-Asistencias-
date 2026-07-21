using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class GruposDTO
    {
        public string? CodigoGrupo { get; set; }
        [Display(Name = "Ingrese el Id de su Asignatura")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdAsignatura { get; set; }
        [Display(Name = "Ingrese su Id")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? IdDocente { get; set; }
        [StringLength(10)]
        [Display(Name = "Ingrese la seccion")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Seccion { get; set; }
    }
}
