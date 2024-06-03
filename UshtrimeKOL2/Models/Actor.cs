using System.ComponentModel.DataAnnotations;

namespace SOA2024.MovieReview.API.Models
{
    public class Actor
    {
        public int Id { get; set; }
       
        [MaxLength(255)]
        public string Name { get; set; }

        [Range(0, 150)]
        public int Age { get; set; }
         
        public virtual List<Movie> Movies { get; set; }  
    }
}
