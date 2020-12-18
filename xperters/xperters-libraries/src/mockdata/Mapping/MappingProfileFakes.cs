using AutoMapper;
using xperters.domain;
using xperters.entities.Entities;

public class MappingProfileFakes : Profile{

    public MappingProfileFakes(){
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>()
            .ForMember(dest => dest.Users, src => src.Ignore());

    }
}