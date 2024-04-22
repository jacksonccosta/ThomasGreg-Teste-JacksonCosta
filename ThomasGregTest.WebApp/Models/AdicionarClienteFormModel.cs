using System.ComponentModel.DataAnnotations;

namespace ThomasGregTest.WebApp;

public class AdicionarClienteFormModel
{
    [Required(ErrorMessage = "Campo Nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "O Logotipo é obrigatório.")]
    [ValidarImagem(ErrorMessage = "Imagem inválida.")]

    public IFormFile Logotipo { get; set; }
}
