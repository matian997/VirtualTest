using System;
using System.Collections.Generic;

namespace VirtualTest.Domain
{
    public class Test
    {
        public virtual int Id { get; set; }
        public virtual double Score { get; set; }
        public virtual int Amount { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        public virtual Category Category { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; } = new List<Question>();
        public virtual double Duration => (this.EndDate - this.StartDate).TotalMinutes;
        
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
            return this.GetCorrectAnswers() / this.Amount) * (int) this.Difficulty;
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
