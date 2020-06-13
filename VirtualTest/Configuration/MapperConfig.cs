using AutoMapper;
using VirtualTest.Profiles;

namespace VirtualTest.Configuration
{
    public class MapperConfig
    {
        private static IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestProfile>();
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<QuestionProfile>();
            });

            return mapperConfig.CreateMapper();
        }

        public static IMapper Instance
        {
            get
            {
                return CreateMapper();
            }
        }
    }
}
