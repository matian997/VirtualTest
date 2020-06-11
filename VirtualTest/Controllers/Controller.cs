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
        private static readonly Lazy<Controller> instance = new Lazy<Controller>(() => new Controller());
        
        private Context context;

        private UserManager userManager;
        private TestManager testManager;
        private CategoryManager categoryManager;

        private IMapper mapper;

        private CommonServices commonServices;

        private User currentUser;
        private Test currentTest;

        private Controller()
        {
            context = Context.Instance;
            
            userManager = new UserManager(context);
            testManager = new TestManager(context);
            categoryManager = new CategoryManager(context);

            commonServices = new CommonServices();

            //mapper = new Mapper();
        }

        public static Controller ControllerInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void NewUser(string userName, string password)
        {
            userManager.NewUser(userName, password);
        }

        public void SignIn(string userName, string password)
        {
            var user = userManager.GetByUserName(userName);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Password == password)
            {
                currentUser = user;
            }
        }

        public void NewTest (int amount, int categoryId, string difficulty)
        {
            var category = categoryManager.GetById(categoryId);
            
            var questions = commonServices.GetTestQuestions(amount, category.Name, difficulty);

            currentTest = new Test
            {
                Amount = amount,
                //Difficulty = Difficulty,
                Category = category,
                Questions = questions,
                User = currentUser
            };
        }

        public void FinishTest()
        {
            currentTest.Finish();
            testManager.Add(currentTest);
        }

        public void SatrtTest()
        {
            currentTest.Start();
        }

        public bool Intent (int idQuestion, string answer)
        {
            var result = false;

            var question = currentTest.Questions.Where(x => x.Id == idQuestion).First();

            if (question.CorrectAnswer == answer)
            {
                result = true;

                question.Result = Result.Correct;

                // Do thing....
            }
            else
            {
                question.Result = Result.Incorrect;

                //Do thing...
            }

            
            return result;
        }
    }
}
