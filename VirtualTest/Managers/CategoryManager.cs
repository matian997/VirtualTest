using VirtualTest.Domain;
using VirtualTest.Mapping;

namespace VirtualTest.Managers
{
    public class CategoryManager : BaseManager<Category, ContextDb>
    {
        public CategoryManager(ContextDb context) : base(context) { }
    }
}
