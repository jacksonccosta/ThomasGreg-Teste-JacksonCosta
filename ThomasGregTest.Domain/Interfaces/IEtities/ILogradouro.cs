namespace ThomasGregTest.Domain;

public interface ILogradouro
{
  int Id { get; set; }
  string Endereco { get; set; }
  string Numero { get; set; }
  string Bairro { get; set; }
  string Cidade { get; set; }
  string Estado { get; set; }
  string Cep { get; set; }
}