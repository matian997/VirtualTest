using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VirtualTest.Domain;

namespace VirtualTest.Services
{
    public class CommonServices
    {
        public IEnumerable<Question> GetTestQuestions(int amount, string category, string dificulty)
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
                        Id= responseItem.id,
                        Description = responseItem.question,
                        CorrectAnswer = responseItem.correct_answer,
                        IncorrectAnswers = responseItem.incorrect_answer
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>();

            var url = "https://opentdb.com/api_category.php";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                dynamic responseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

                foreach (var responseItem in responseJSON.results)
                {
                    var category = new Category
                    {
                        Id = responseItem.id,
                        Name = responseItem.name
                    };

                    categories.Add(category);
                }
            }

            return categories;
        }
    }
}
