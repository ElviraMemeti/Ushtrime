namespace SOA2024.MovieReview.API.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Rating { get; set; }  

        public Guid MovieId { get; set; } 
        public virtual Movie Movie { get; set; }    
    }
}
