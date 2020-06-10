using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VirtualTest.Domain;

namespace VirtualTest.Managers
{
    public class CategoryManager : BaseManager<Category,Context>
    {
        public CategoryManager(Context context) : base(context) { }

        public override void Add(Category category)
        {
            this.context.Set<Category>().Add(category);
            this.context.SaveChanges();
        }
    }
}
