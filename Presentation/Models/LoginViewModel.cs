using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El carnet es obligatorio")]
        public string Carnet { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}