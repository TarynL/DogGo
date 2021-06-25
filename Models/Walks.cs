using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walks
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public Walker Walker { get; set; }
        public Dog Dog { get; set; }

        public Owner Owner { get; set; }

        public string DateString()
        {
            return Date.ToShortDateString();
        }
        //public int hrs { get; set; }
        //public int mins { get; set; }

        //public string TotalTime()
        //{
        //    hrs = Duration / 60;
        //    mins = Duration % 3600;
        //    Console.WriteLine($"{hrs} hrs {mins} mins");
        //}



    }
}
