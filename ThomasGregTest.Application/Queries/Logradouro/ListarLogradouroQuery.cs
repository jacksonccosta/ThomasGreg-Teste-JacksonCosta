using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class ListarLogradouroQuery(int clienteId) : Request<IEvento>
{
    public int ClienteId { get; private set; } = clienteId;
}
