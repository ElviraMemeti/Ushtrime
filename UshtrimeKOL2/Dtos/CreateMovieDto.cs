using SOA2024.MovieReview.API.Helpers;
using SOA2024.MovieReview.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA2024.MovieReview.API.Dtos
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; } 
        public int DirectorId { get; set; }
    }
}
