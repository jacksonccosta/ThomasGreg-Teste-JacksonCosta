using System.ComponentModel.DataAnnotations;

namespace ThomasGregTest.WebApp;

public class ClienteViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = null!;

    public string Logotipo { get; set; } = null!;

}
