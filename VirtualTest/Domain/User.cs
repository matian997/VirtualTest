using System.Collections.Generic;

namespace VirtualTest.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Name { get; set; }
        public virtual string LastName { get; set; }
        public virtual IList<Test> Tests { get; set; } = new List<Test>();

        public void AddTest(Test test)
        {
            this.Tests.Add(test);
        }
    }
}
