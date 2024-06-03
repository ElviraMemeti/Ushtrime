using System.ComponentModel.DataAnnotations;

namespace SOA2024.MovieReview.API.Models
{
    public class Director
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        public int Age { get ; set; }
        public string Biography { get; set; }
    }
}
