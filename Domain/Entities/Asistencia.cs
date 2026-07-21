using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        [Display(Name ="Id Sesion")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdSesion {  get; set; }
        [Display(Name = "Id Carnet")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Carnet { get; set; }
        [StringLength(15)]
        [Display(Name ="Estado de asistencia")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Estado { get; set; }
        [Display(Name = "Hora de Registro")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public TimeOnly Hora_Registro { get; set; }

        
    }
}
