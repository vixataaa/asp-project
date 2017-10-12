using AutoMapper;

namespace SecondHand.Web.Infrastructure
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
