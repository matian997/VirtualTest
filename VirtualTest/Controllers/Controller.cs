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
        
        private ApplicationContext context;

        // publicos para hacer pruebas
        public UserManager userManager;
        public TestManager testManager;
        public CategoryManager categoryManager;

        private IMapper mapper;
        private IConnectionService connectionServices;
        private IStrategyScore strategyScore;
        private IEncryptionService encryptionService;

        private User currentUser;
        private Test currentTest;

        private Controller()
        {
            context = ApplicationContext.Instance;
            
            userManager = new UserManager(context);
            testManager = new TestManager(context);
            categoryManager = new CategoryManager(context);

            mapper = MapperConfig.Instance;
            connectionServices = new ConnectionServices();
            strategyScore = new StrategyScore();
            encryptionService = new EncryptionService();

            //Carga de categories
            var categories = connectionServices.GetAllCategories();
            foreach(var category in categories)
            {
                categoryManager.Add(category);
            }
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
            var encryptedPassword = encryptionService.Encrypt(password);

            userManager.NewUser(userName, encryptedPassword);
        }

        public UserDTO LogIn(string userName, string password)
        {
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

        public void LogOut()
        {
            // Do something...
        }

        public void NewTest(int amount, int categoryId, Difficulty difficulty)
        {   
            var questions = connectionServices.GetTestQuestions(amount, categoryId, difficulty.ToString());

            currentTest = new Test
            {
                Amount = amount,
                Difficulty = difficulty,
                Category = categoryManager.GetById(categoryId),
                Questions = questions,
                //User = currentUser
            };
        }

        public TestLiteDTO FinishTest()
        {
            currentTest.Finish();

            var amountCorrectAwnwers = currentTest.GetAmountCorrectAnwers();

            currentTest.Score = strategyScore.GetScoreByOpenTDB(amountCorrectAwnwers, currentTest.Amount, currentTest.Difficulty, currentTest.Duracion);

            testManager.Add(currentTest);

            return mapper.Map<TestLiteDTO>(currentTest);
        }

        public IEnumerable<QuestionDTO> SatrtTest()
        {
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
            var tests = testManager.GetAll();

            var topTests = tests.OrderByDescending(x => x.Score).Take(10);

            return mapper.Map<IEnumerable<TestLiteDTO>>(topTests);
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var allCategories = categoryManager.GetAll();

            return mapper.Map<IEnumerable<CategoryDTO>>(allCategories);
        }
    }
}
