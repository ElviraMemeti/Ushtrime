using SOA2024.MovieReview.API.Models;

namespace UshtrimeKOL2.Interfaces
{
    public class IMovieRepository
    {
        public List<Movie> GetAll(int pageNumber, int pageSize, Genre? genre, string search, string? sortBy, bool ascending = true);
        public List<Movie> GetAll();
        public int TotalCount(Genre? genre, string search);
        public Movie GetById(Guid id);
        public void Create(Movie movie);
        public void Update(Movie movie);
        public void Delete(Guid id);
        public bool SaveChanges();
    }
}
