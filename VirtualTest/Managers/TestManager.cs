using VirtualTest.Domain;

namespace VirtualTest
{
    public class TestManager : BaseManager<Test, Context>
    {
        public TestManager(Context context) : base(context) { }

        public override void Add(Test test)
        {
            this.context.Set<Test>().Add(test);
            this.context.SaveChanges();
        }
    }
}
