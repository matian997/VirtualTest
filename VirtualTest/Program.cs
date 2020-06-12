using System;
using System.Collections.Generic;
using System.Linq;
using VirtualTest.Controllers;
using VirtualTest.Domain;

namespace VirtualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Controller.ControllerInstance;

            x.NewUser("matias", "tupasbourd");

            IEnumerable<User> users = x.userManager.GetAll();

            foreach (var user in users)
            {
                Console.WriteLine(user.Id);
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.Password);
            }

           

            Console.ReadKey();
        }
    }
}
