using AutoMapper;
using VirtualTest.Domain;
using VirtualTest.DTO;

namespace VirtualTest.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDTO>();
            CreateMap<Test, TestLiteDTO>();
        }
    }
}
