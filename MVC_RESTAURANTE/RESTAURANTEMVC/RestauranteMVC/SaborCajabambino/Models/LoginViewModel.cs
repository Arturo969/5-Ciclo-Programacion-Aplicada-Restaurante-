using System.ComponentModel.DataAnnotations;

namespace SaborCajabambino.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}
