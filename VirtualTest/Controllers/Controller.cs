using System;
using VirtualTest.Domain;
using VirtualTest.DTO;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using VirtualTest.Services;
using VirtualTest.Managers;
using VirtualTest.Services.Interfaces;
using VirtualTest.Mapping;

namespace VirtualTest.Controllers
{
    public class Controller
    {
        private static readonly Lazy<Controller> instance = new Lazy<Controller>(() => new Controller());
        
        private ContextDb context;

        // publicos para hacer pruebas
        public UserManager userManager;
        public TestManager testManager;
        public CategoryManager categoryManager;

        private IMapper mapper;

        private IConnectionService conecctionServices;
        private IStrategyScore strategyScore;

        private User currentUser;
        private Test currentTest;

        private Controller()
        {
            context = ContextDb.Instance;
            
            userManager = new UserManager(context);
            testManager = new TestManager(context);
            categoryManager = new CategoryManager(context);

            conecctionServices = new ConnectionServices();
            strategyScore = new StrategyScore();

            //mapper = new Mapper();
        }

        public static Controller ControllerInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void NewUser (string userName, string password)
        {
            userManager.NewUser(userName, password);
        }

        public void SignIn (string userName, string password)
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
            
            var questions = conecctionServices.GetTestQuestions(amount, category.Name, difficulty);

            currentTest = new Test
            {
                Amount = amount,
                //Difficulty = Difficulty,
                Category = category,
                Questions = questions,
                User = currentUser
            };
        }

        public TestDTO FinishTest ()
        {
            currentTest.Finish();

            var amountCorrectAwnwers = currentTest.GetAmountCorrectAnwers();

            //currentTest.Score = strategyScore.GetScoreByOpenTDB(amountCorrectAwnwers, currentTest.Amount, currentTest.Difficulty, currentTest.Duracion);

            testManager.Add(currentTest);

            return mapper.Map<TestDTO>(currentTest);
        }

        public IEnumerable<QuestionDTO> SatrtTest()
        {
            currentTest.Start();

            return mapper.Map<IEnumerable<QuestionDTO>>(currentTest.Questions);
        }

        public bool Try (int idQuestion, string answer)
        {
            var result = false;

            var question = currentTest.Questions.Where(x => x.Id == idQuestion).First();

            if (question.CorrectAnswer == answer)
            {
                result = true;

                question.Result = Result.Correct;

                // Do something....
            }
            else
            {
                question.Result = Result.Incorrect;

                //Do something...
            }

            return result;
        }

        public IEnumerable<TestDTO> TopTenTest ()
        {
            var tests = testManager.GetAll();

            tests.OrderByDescending(x => x.Score).Take(10);

            return mapper.Map<IEnumerable<TestDTO>>(tests);
        }
    }
}
