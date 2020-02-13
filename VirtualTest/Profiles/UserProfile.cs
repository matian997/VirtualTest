using AutoMapper;
using VirtualTest.Domain;
using VirtualTest.DTO;

namespace VirtualTest.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
