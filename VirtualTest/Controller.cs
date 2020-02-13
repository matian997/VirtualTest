using System;
using VirtualTest.Domain;
using VirtualTest.DTO;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

namespace VirtualTest
{
    public class Controller
    {
        private readonly UserManager userManager;
        private readonly TestManager testManager;
        private readonly EFConfing dbContext;
        private static readonly IMapper mapper;
        private User user;
        private Test test;

        public Controller(EFConfing dbContext)
        {
            if (dbContext == null)
            {
                throw new NotImplementedException();
            }

            this.dbContext = dbContext;
            this.userManager = new UserManager(dbContext);
            this.testManager = new TestManager(dbContext);
        }

        public bool NewUser(string userName, string name, string lastName)
        {
            var user = new User
            {
                UserName = userName,
                Name = name,
                LastName = lastName
            };
            
            return this.userManager.Add(user);
        }

        public void NewTest (int amount, Category category, Difficulty difficulty)
        {
            this.test = new Test(amount, category, difficulty);
        }

        public TestDTO IniciarTest()
        {
            this.test.Start();
            return this.test;
        }

        public void FinalizarTest()
        {
            this.test.Finish();
            this.user.AddTest(test);
            this.testManager.Add(test);
        }

        // question no tiene id tengo que buscar otra manera de como identificar una pregunta
        public bool Intento(int idQuestion, string answer)
        {
            var question = this.test.Questions
                .Where(x => x.Id == idQuestion)
                .First();

            if (question.CorrectAnswer == answer)
            {
                question.Result = true;

                return true;
            }

            question.Result = false;

            return false;
        }

        public IEnumerable<TestDTO> MejoresTest()
        {
            var tests = this.testManager.GetAll()
                .OrderByDescending(test => test.Score)
                .ToList();

            return tests;
        }

    }
}
