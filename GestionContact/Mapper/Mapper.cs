using AutoMapper;
using GestionContact.Models;
using GestionContact.ViewModels;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;

namespace GestionContact.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<LoginDto, ViewLoginApi>();
            CreateMap<RegisterDto, ViewRegisterApi>();
            CreateMap<User, UserApi>();
            CreateMap<Role, RoleApi>();
            CreateMap<Contact, ContactApi>();
            CreateMap<Adresse, AdresseApi>();
            CreateMap<ContactDto, ViewContact>();
            CreateMap<Contact, ContactDto>();
            CreateMap<Image, ImageApi>();
            // Resource to Domain
            CreateMap<ViewLoginApi, LoginDto>();
            CreateMap<ViewRegisterApi, RegisterDto>();
            CreateMap<UserApi, User>();
            CreateMap<RoleApi, Role>();
            CreateMap<ContactApi, Contact>();
            CreateMap<AdresseApi, Adresse>();
            CreateMap<ViewContact, ContactDto>();
            CreateMap<ContactDto, Contact>();
            CreateMap<ImageApi, Image>();
        }
    }
}

