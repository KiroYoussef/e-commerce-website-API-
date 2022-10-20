using AutoMapper;

using Electronic.Api.Model;
using Electronic.Api.Model.user;


namespace Electronic.Api.Profiles
{
    public class RepsitoryProfiles : Profile
    {

        public RepsitoryProfiles()
        {



            CreateMap<ApplicationUser, AddUserModel>()
           .ForMember(dst => dst.UserName, src => src.MapFrom(c => c.UserName))
           .ForMember(dst => dst.Email, src => src.MapFrom(c => c.Email))
           .ForMember(dst => dst.EmailConfirmed, src => src.MapFrom(c => c.EmailConfirmed))
           .ForMember(dst => dst.Address, src => src.MapFrom(c => c.Address))
           .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(c => c.PhoneNumber))
           .ReverseMap();










        }


    }
}
//.ForMember(dst => dst.ActorName, src => src.MapFrom(c => c.Actor.ActorName))
//.ForMember(dst => dst.ActorPictier, src => src.MapFrom(c => c.Actor.ActorPicture))