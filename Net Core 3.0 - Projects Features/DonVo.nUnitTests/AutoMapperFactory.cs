using AutoMapper;
using DonVo.SpecialConfigurations;

namespace DonVo.nUnitTests
{
    public class AutoMapperFactory
    {
        public IMapper Create()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            return new Mapper(configuration);
        }
    }
}
