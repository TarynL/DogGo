using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        [DisplayName("Dog Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        [DisplayName("Owner")]
        public int OwnerId { get; set; }

        [Required]
        [DisplayName("Breed")]
        public string Breed { get; set; }


        [DisplayName("Notes")]
        public string Notes { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public Owner Owner { get; set; }


    }
}
