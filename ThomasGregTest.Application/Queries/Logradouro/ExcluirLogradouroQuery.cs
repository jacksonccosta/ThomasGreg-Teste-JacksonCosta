using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class ExcluirLogradouroQuery(int id, int clienteId) : Request<IEvento>
{
    public int Id { get; private set; } = id;
    public int ClienteId { get; private set; } = clienteId;
}
