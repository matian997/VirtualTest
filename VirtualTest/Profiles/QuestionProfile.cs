using AutoMapper;
using VirtualTest.Domain;
using VirtualTest.DTO;

namespace VirtualTest.Profiles
{
    class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDTO>();
        }
    }
}
