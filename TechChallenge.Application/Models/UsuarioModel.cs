using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TechChallenge.Application.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de email válido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordStrongValidation(ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string SenhaConfirmacao { get; set; }
    }
    public class PasswordStrongValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string password = value.ToString();

            bool hasUpperCase = new Regex(@"[A-Z]").IsMatch(password);
            bool hasLowerCase = new Regex(@"[a-z]").IsMatch(password);
            bool hasDigit = new Regex(@"[0-9]").IsMatch(password);
            bool hasSpecialChar = new Regex(@"[^A-Za-z0-9]").IsMatch(password);

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
    }
}
