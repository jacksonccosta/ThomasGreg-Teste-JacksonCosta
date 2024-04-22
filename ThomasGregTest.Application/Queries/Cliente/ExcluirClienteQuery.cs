using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class ExcluirClienteQuery(int id) : Request<IEvento>
{
    public int Id { get; private set; } = id;
}
