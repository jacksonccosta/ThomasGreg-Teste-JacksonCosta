using System.ComponentModel.DataAnnotations;

namespace ThomasGregTest.WebApp;

public class UsuarioCadastroModel
{
    [Required(ErrorMessage = "Campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo Senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
}
