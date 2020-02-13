using VirtualTest.Domain;

namespace VirtualTest
{
    public class TestManager : Manager<Test, EFConfing>
    {
        public TestManager(EFConfing dbContext) : base(dbContext) { }

        public override bool Add(Test test)
        {
            if (test != null)
            {
                this.dbContext.Set<Test>().Add(test);
                this.dbContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
