using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VirtualTest.Domain;
using VirtualTest.Services.Interfaces;

namespace VirtualTest.Services
{
    public class ConnectionServices : IConnectionService
    {
        public IEnumerable<Question> GetTestQuestions(int amount, int category, string dificulty)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var questions = new List<Question>();

            var url = "https://opentdb.com/api.php?amount="+ amount 
                + "&category=" + category
                + "&difficulty=" + dificulty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                dynamic responseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

                foreach (var responseItem in responseJSON.results)
                {
                    var question = new Question
                    {
                        Description = responseItem.question,
                        CorrectAnswer = responseItem.correct_answer,
                        IncorrectAnswers = responseItem.incorrect_answers.ToObject<List<string>>()
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            var url = "https://opentdb.com/api_category.php";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                dynamic responseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

                foreach (var responseItem in responseJSON.trivia_categories)
                {
                    var category = new Category
                    {
                        No = responseItem.id,
                        Name = responseItem.name
                    };

                    categories.Add(category);
                }
            }

            return categories;
        }
    }
}
