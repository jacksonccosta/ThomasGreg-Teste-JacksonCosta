using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class SelecionarClienteQuery(int id) : Request<IEvento>
{
    public int Id { get; private set; } = id;
}
