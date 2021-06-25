using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walks
    {
        public int Id { get; set; }

        [DisplayName("Walk Date")]
        [Required(ErrorMessage = "Please enter a valid date. MM/DD/YYYY")]
        [DisplayFormat(DataFormatString ="{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }


        [DisplayName("Walk Duration")]
        [Required]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Please enter a valid name.")]
        [DisplayName("Walker Name")]
        public int WalkerId { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        [DisplayName("Dog Name")]
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
