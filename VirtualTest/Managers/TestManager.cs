using VirtualTest.Domain;

namespace VirtualTest
{
    public class TestManager : BaseManager<Test, Context>
    {
        public TestManager(Context context) : base(context) { }
    }
}
