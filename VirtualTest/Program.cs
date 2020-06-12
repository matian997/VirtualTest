using System;
using VirtualTest.Controllers;

namespace VirtualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Controller.ControllerInstance;

            x.NewUser("matias", "tupasbourd");

            var users = x.userManager.GetAll();

            Console.WriteLine(users);

            Console.ReadKey();
        }
    }
}
