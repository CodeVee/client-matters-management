using Application.DataTransferObjects;
using AutoMapper;
using Domain.Entities;

namespace WebAPI.Profiles
{
    public class MatterProfile : Profile
    {
        public MatterProfile()
        {
            CreateMap<Matter, MatterListDTO>()
                .ForMember(dest => dest.Code, source => source.MapFrom(s => s.MatterCode))
                .ForMember(dest => dest.Title, source => source.MapFrom(s => s.MatterTitle));

            CreateMap<CreateMatterDTO, Matter>()
                .ForMember(dest => dest.MatterTitle, source => source.MapFrom(s => s.Title))
                .ForMember(dest => dest.MatterCode, source => source.MapFrom(s => s.Code));

            CreateMap<UpdateMatterDTO, Matter>()
                .ForMember(dest => dest.MatterTitle, source => source.MapFrom(s => s.Title))
                .ReverseMap();
        }
    }
}
