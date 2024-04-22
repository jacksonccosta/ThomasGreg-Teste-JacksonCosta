using AutoMapper;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public class ClienteMapper<T> where T : class
{
    public static T Map(object source)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cliente, AdicionarClienteQuery>().ReverseMap();
            cfg.CreateMap<Cliente, AtualizarClienteQuery>().ReverseMap();
            cfg.CreateMap<Cliente, ClienteResponse>().ReverseMap();
        });
        IMapper iMapper = config.CreateMapper();

        return iMapper.Map<T>(source);
    }
}
