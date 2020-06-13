using AutoMapper;
using VirtualTest.Domain;
using VirtualTest.DTO;

namespace VirtualTest.Profiles
{
    class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
