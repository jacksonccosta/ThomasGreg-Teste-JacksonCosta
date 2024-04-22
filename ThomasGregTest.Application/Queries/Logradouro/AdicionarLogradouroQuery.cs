using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class AdicionarLogradouroQuery(int clienteId,
    string? endereco,
    string? numero,
    string? bairro,
    string? cidade,
    string? estado,
    string? cep) : Request<IEvento>
{
    public int ClienteId { get; private set; } = clienteId;
    public string? Endereco { get; private set; } = endereco;
    public string? Numero { get; private set; } = numero;
    public string? Bairro { get; private set; } = bairro;
    public string? Cidade { get; private set; } = cidade;
    public string? Estado { get; private set; } = estado;
    public string? Cep { get; private set; } = cep;
}
