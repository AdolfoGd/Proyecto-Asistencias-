using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Grupos
    {
        public string? CodigoGrupo { get; set; }

        [Display(Name = "Ingrese la Asignatura")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? IdAsignatura { get; set; }

        [Display(Name = "Ingrese el Carnet/Id del Docente")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? IdDocente { get; set; }

        [StringLength(10)]
        [Display(Name = "Ingrese la sección")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Seccion { get; set; }
    }
}