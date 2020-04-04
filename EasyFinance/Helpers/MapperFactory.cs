using AutoMapper;
using EasyFinance.Profiles;

namespace EasyFinance.Helpers
{
    public interface IMapperFactory
    {
        IMapper CreateMapper();
    }

    public class MapperFactory: IMapperFactory
    {
        public IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReceiptProfile>();
            });
            return new Mapper(config);
        }
    }
}
