using AutoMapper;
using InfrontAssesment.Core.Dtos;
using InfrontAssesment.Core.Models;

namespace InfrontAssesment.WebUI
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Stock, StockDto>()
               .ForMember(s => s.Symbol, o => o.MapFrom(s => s.Symbol))
               .ForMember(s => s.BuyValue, o => o.MapFrom(o => o.BuyValue))
               .ForMember(s => s.NumberOfContracts, o => o.MapFrom(o => o.NumberOfContracts));

            CreateMap<StockDto, Stock>()
               .ForMember(s => s.Symbol, o => o.MapFrom(s => s.Symbol))
               .ForMember(s => s.BuyValue, o => o.MapFrom(o => o.BuyValue))
               .ForMember(s => s.NumberOfContracts, o => o.MapFrom(o => o.NumberOfContracts));
        }
    }
}
