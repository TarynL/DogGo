using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        [DisplayName("Walker Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }


        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}

