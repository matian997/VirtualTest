using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VirtualTest.Controllers;
using VirtualTest.Domain;
using VirtualTest.DTO;

namespace VirtualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Controller.ControllerInstance;

            //x.NewTest(10, 9, Difficulty.easy);

            //var y = x.SatrtTest();

            //foreach(var q in y)
            //{
            //    Console.Write("Pregunta: ");
            //    Console.WriteLine(q.Description);
            //    Console.Write("Respuestas: ");
            //    Console.WriteLine(q.CorrectAnswer);
            //    Console.Write("------------------");

            //    Console.ReadKey();
            //}

            //var categories = x.GetAllCategories();

            //foreach(var category in categories)
            //{
            //    Console.Write("Id: ");
            //    Console.WriteLine(category.No);
            //    Console.Write("Name: ");
            //    Console.WriteLine(category.Name);

            //    x.categoryManager.Remove(category.Id);
            //}

            Console.Write("Listo");



            Console.ReadKey();
        }
    }
}
