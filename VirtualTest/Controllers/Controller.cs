using System;
using VirtualTest.Domain;
using VirtualTest.DTO;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using VirtualTest.Services;
using VirtualTest.Managers;
using VirtualTest.Services.Interfaces;
using VirtualTest.Configuration;

namespace VirtualTest.Controllers
{
    public class Controller
    {
        private static readonly Lazy<Controller> instance = new Lazy<Controller>(() => new Controller());
        private User currentUser;
        private Test currentTest;

        private Controller()
        {
            /*//Carga de categories
            var categories = connectionServices.GetAllCategories();
            foreach(var category in categories)
            {
                categoryManager.Add(category);
            }*/
        }

        public static Controller ControllerInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void SignIn(string userName, string password)
        {
            using (var dbContext = new ApplicationContext())
            { 
                IEncryptionService encryptionService = new EncryptionTripleDES();

                UserManager userManager = new UserManager(dbContext);

                var encryptedPassword = encryptionService.Encrypt(password);

                userManager.NewUser(userName, encryptedPassword);   
            }
        }
        public UserDTO LogIn(string userName, string password)
        {
            using (var dbContext = new ApplicationContext())
            {
                UserManager userManager = new UserManager(dbContext);
                IEncryptionService encryptionService = new EncryptionTripleDES();
                IMapper mapper = MapperConfig.Instance;

                var user = userManager.GetByUserName(userName);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var dencryptedPassword = encryptionService.Dencrypt(user.Password);

                if (dencryptedPassword == password)
                {
                    currentUser = user;
                }

                return mapper.Map<UserDTO>(user);
            }
        }

        public void LogOut()
        {
            // Do something...
        }

        public void NewTest(int amount, int categoryId, Difficulty difficulty)
        {   
            using (var dbContext = new ApplicationContext())
            {
                using (IConnectionService connectionServices = new ConnectionServicesOpenTdb())
                {
                    var questions = connectionServices.GetTestQuestions(amount, categoryId, difficulty.ToString());
                    CategoryManager categoryManager = new CategoryManager(dbContext);
                    currentTest = new Test
                    {
                        Amount = amount,
                        Difficulty = difficulty,
                        Category = categoryManager.GetById(categoryId),
                        Questions = questions,
                        User = currentUser
                    };
                }
            }
        }

        public TestLiteDTO FinishTest()
        {
            using (var dbContext = new ApplicationContext())
            {
                IStrategyScore strategyScore = new StrategyScoreOpenTdb();
                TestManager testManager = new TestManager(dbContext);
                IMapper mapper = MapperConfig.Instance;

                currentTest.Finish();

                var amountCorrectAwnwers = currentTest.GetAmountCorrectAnwers();

                currentTest.Score = strategyScore.GetScore(amountCorrectAwnwers, currentTest.Amount, currentTest.Difficulty, currentTest.Duracion);

                testManager.Add(currentTest);

                return mapper.Map<TestLiteDTO>(currentTest);
            }
        }

        public IEnumerable<QuestionDTO> StartTest()
        {
            IMapper mapper = MapperConfig.Instance;
            currentTest.Start();

            return mapper.Map<IEnumerable<QuestionDTO>>(currentTest.Questions);
        }

        public bool Try(int idQuestion, string answer)
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

        public void CancelTest()
        {
            // Do something
        }

        public IEnumerable<TestLiteDTO> TopTenTest()
        {
            using (var dbContext = new ApplicationContext())
            {
                TestManager testManager = new TestManager(dbContext);
                IMapper mapper = MapperConfig.Instance;

                var tests = testManager.GetAll();

                var topTests = tests.OrderByDescending(x => x.Score).Take(10);

                return mapper.Map<IEnumerable<TestLiteDTO>>(topTests);
            }
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            using (var dbContext = new ApplicationContext())
            {
                CategoryManager categoryManager = new CategoryManager(dbContext);
                IMapper mapper = MapperConfig.Instance;

                var allCategories = categoryManager.GetAll();

                return mapper.Map<IEnumerable<CategoryDTO>>(allCategories);
            }
        }
    }
}
