using VirtualTest.Configuration;
using VirtualTest.Domain;
namespace VirtualTest.Managers
{
    public class TestManager : BaseManager<Test, ApplicationContext>
    {
        public TestManager(ApplicationContext context) : base(context) { }
    }
}
