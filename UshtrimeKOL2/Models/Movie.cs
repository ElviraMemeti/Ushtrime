using SOA2024.MovieReview.API.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA2024.MovieReview.API.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }   
        public Genre Genre { get; set; }

        [ForeignKey(nameof(Director))]
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }    

        
        public virtual List<Actor> Actors { get; set; } 
        public virtual List<Review> MovieReviews { get; set; }
    }
}
