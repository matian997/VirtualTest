using System;
using VirtualTest.Domain;
using VirtualTest.DTO;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using VirtualTest.Services;
using VirtualTest.Managers;

namespace VirtualTest
{
    public class Controller
    {
        private readonly CommonServices commonServices;
        private readonly UserManager userManager;
        private readonly TestManager testManager;
        private readonly CategoryManager categoryManager;
        private readonly Context dbContext;
        private static readonly IMapper mapper;
        private User currentUser;
        private Test currentTest;

        public Controller(Context dbContext)
        {
            if (dbContext == null)
            {
                throw new NotImplementedException();
            }

            this.dbContext = dbContext;
            this.userManager = new UserManager(dbContext);
            this.testManager = new TestManager(dbContext);
        }

        public void NewTest (int amount, int categoryId, string difficulty)
        {
            var category = categoryManager.GetById(categoryId);
            
            var questions = commonServices.GetTestQuestions(amount, category.Name, difficulty);

            this.currentTest = new Test()
            {
                Amount = amount,
                Difficulty = category,
                Category = category,
                Questions = questions,
                User = this.currentUser
            };
        }
    }
}
