using AutoMapper;
using VirtualTest.Domain;
using VirtualTest.DTO;

namespace VirtualTest.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestLiteDTO>()
                .ForMember(x => x.UserName, y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Date, y => y.MapFrom(x => x.StartDate));
        }
    }
}
