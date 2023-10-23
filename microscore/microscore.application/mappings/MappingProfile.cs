using AutoMapper;
using microscore.application.models.dtos.accounts;
using microscore.application.models.dtos.people;
using microscore.domain.entities.Accounts;
using microscore.domain.entities.People;

namespace microscore.application.mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonNav.Id))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.PersonNav.Address))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PersonNav.Name))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.PersonNav.Identification))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PersonNav.Phone))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientNav.PersonNav.Name))
                .ReverseMap();
        }

    }
}
