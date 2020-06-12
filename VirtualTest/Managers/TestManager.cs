using VirtualTest.Domain;
using VirtualTest.Mapping;

namespace VirtualTest.Managers
{
    public class TestManager : BaseManager<Test, ContextDb>
    {
        public TestManager(ContextDb context) : base(context) { }
    }
}
