namespace SOA2024.MovieReview.API.Dtos
{
    public class MovieDto
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; } 
        public string DirectorName { get; set; }
       // public List<string> Actors { get; set; }
    }
}
