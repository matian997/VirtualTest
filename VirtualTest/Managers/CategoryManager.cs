using VirtualTest.Domain;

namespace VirtualTest.Managers
{
    public class CategoryManager : BaseManager<Category,Context>
    {
        public CategoryManager(Context context) : base(context) { }
    }
}
