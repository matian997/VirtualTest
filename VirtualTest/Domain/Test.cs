using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace VirtualTest.Domain
{
    public class Test
    {
        public virtual int Id { get; set; }
        public virtual double Score { get; set; }
        public virtual int Amount { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual User Owner { get; set; }
        public virtual IList<Question> Questions { get; set; } = new List<Question>();
        public virtual double Duration => (this.EndDate - this.StartDate).TotalMinutes;

        public Test(int amount, Category category, Difficulty dificulty)
        {
            this.Amount = amount;
            this.Difficulty = dificulty;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var url = "https://opentdb.com/api.php?amount="
                + amount + "&category="
                + category + "&difficulty="
                + dificulty;

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
                        IncorrectAnswers = responseItem.incorrect_answer
                    };
                    this.Questions.Add(question);
                }
            }
        }

        public void Start()
        {
            this.StartDate = DateTime.Now;
        }

        public void Finish()
        {
            this.EndDate = DateTime.Now;
            this.Score = this.GetScore();
        }

        private double GetScore()
        {
            return (this.GetCorrectAnswers() / this.Amount) * this.Difficulty;
        }

        private int GetCorrectAnswers()
        {
            int result = 0;
            for (int i = 0; i <= this.Questions.Count; ++i) 
            {
                if (this.Questions[i].Result == true)
                {
                    ++result;
                }
            }

            return result;
        }
    }
}
