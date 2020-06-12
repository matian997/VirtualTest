using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualTest.Domain
{
    public class Test
    {
        public virtual int Id { get; set; }
        public virtual double Score { get; set; }
        public virtual int Amount { get; set; }
        public virtual double Duracion { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        public virtual Category Category { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; } = new List<Question>();

        //public double Duracion
        //{
        //    get
        //    {
        //        return (EndDate - StartDate).TotalMinutes;
        //    }
        //}
        
        public void Start()
        {
            StartDate = DateTime.Now;
        }

        public void Finish()
        {
            EndDate = DateTime.Now;
        }

        public int GetAmountCorrectAnwers()
        {
            var amount = Questions.Where(x => x.Result == Result.Correct).Count();

            return amount;
        }
    }
}
