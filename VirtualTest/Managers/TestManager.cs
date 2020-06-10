using VirtualTest.Domain;

namespace VirtualTest
{
    public class TestManager : BaseManager<Test, Context>
    {
        public TestManager(Context dbContext) : base(dbContext) { }

        public override void Add(Test test)
        {
            this.context.Set<Test>().Add(test);
            this.context.SaveChanges();
        }
    }
}
