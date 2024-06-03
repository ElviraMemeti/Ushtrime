using SOA2024.MovieReview.API.Models;

namespace SOA2024.MovieReview.API.ExternalModels
{
    public class TokenResponse
    {
        public string Status { get; set; }
        public TokenData Data { get; set; }
    }

    public class TokenData
    {
        public string Token { get; set; }
    }

    public class TVDBMoviesResult
    {
        public List<TVDBMovie> Data { get; set; }
    }

    public class TVDBMovie
    {
        public string Name { get; set; }
        public int Runtime { get; set; }
        public string Year { get; set; } 
        public int Id { get; set; }
    }
}
