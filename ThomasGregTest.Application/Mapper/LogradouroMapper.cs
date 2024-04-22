using AutoMapper;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public class LogradouroMapper<T> where T : class
{
    public static T Map(object source)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Logradouro, AdicionarLogradouroQuery>().ReverseMap();
            cfg.CreateMap<Logradouro, AtualizarLogradouroQuery>().ReverseMap();
            cfg.CreateMap<Logradouro, LogradouroResponse>().ReverseMap();
        });
        IMapper iMapper = config.CreateMapper();

        return iMapper.Map<T>(source);
    }
}
