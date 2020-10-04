using Application.DataTransferObjects;
using AutoMapper;
using Domain.Entities;

namespace WebAPI.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientListDTO>()
                .ForMember(dest => dest.FullName, source => source.MapFrom(s => s.ClientName))
                .ForMember(dest => dest.Code, source => source.MapFrom(s => s.ClientCode))
                .ForMember(dest => dest.DateRegistered, source => source.MapFrom(s => s.RegistrationDate.ToString("MMM dd, yyyy")));

            CreateMap<Client, ClientDetailDTO>()
                .ForMember(dest => dest.FullName, source => source.MapFrom(s => s.ClientName))
                .ForMember(dest => dest.Code, source => source.MapFrom(s => s.ClientCode))
                .ForMember(dest => dest.DateRegistered, source => source.MapFrom(s => s.RegistrationDate.ToString("MMM dd, yyyy")));

            CreateMap<CreateClientDTO, Client>()
                .ForMember(dest => dest.ClientName, source => source.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(dest => dest.ClientCode, source => source.MapFrom(s => s.Code));

            CreateMap<UpdateClientDTO, Client>()
                .ForMember(dest => dest.ClientName, source => source.MapFrom(s => s.FirstName + " " + s.LastName))
                .ReverseMap();
        }
    }
}
