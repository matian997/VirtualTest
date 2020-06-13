using VirtualTest.Configuration;
using VirtualTest.Domain;

namespace VirtualTest.Managers
{
    public class CategoryManager : BaseManager<Category, ApplicationContext>
    {
        public CategoryManager(ApplicationContext context) : base(context) { }
    }
}
