
using System.Collections.Generic;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }
        //public List<Dog> Dogs { get; internal set; }
        public Dog Dog {  get; set; }

        //public static implicit operator List<object>(Dog v)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
