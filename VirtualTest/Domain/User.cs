using System.Collections.Generic;

namespace VirtualTest.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual IList<Test> Tests { get; set; } = new List<Test>();
    }
}
