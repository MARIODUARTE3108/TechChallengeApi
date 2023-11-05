using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Application.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Endereço de email inválido.")]
        [Required(ErrorMessage = "Informe o email do usuário.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário.")]
        public string Senha { get; set; }
    }
}
