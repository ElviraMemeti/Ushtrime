using SOA2024.MovieReview.API.Helpers;
using SOA2024.MovieReview.API.Models;
using SolrNet.Utils;
using UshtrimeKOL2.Data;
using UshtrimeKOL2.Interfaces;

namespace UshtrimeKOL2.Repository
{
    public class MovieRepository: IMovieRepository
    {
        private MovieContext _movieContext;


        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;   

        }

        public List<Movie> GetAll()
        {
            var movies = _movieContext.Movies.ToList();

            return movies;
        }


        public List<Movie>GetAll(int pageNumber, int pageSize, Genre? genre, string search, string? sortBy, bool ascending =true)
        {
            var movies = _movieContext.Movies
                .Where(x => (genre == null || x.Genre == genre))
                && (string.IsNullOrEmpty(search) || x.Title.Contains(search) || x.Description.Contains(search));

            switch (sortBy)
            {

                case "Title":
                    movies = ascending == true? movies.OrderBy(x => x.Title) : movies.OrderByDescending(x => x.Title);
                    break;


                case "Description":
                    movies = ascending == true ? movies.OrderBy(m => m.Description) : movies.OrderByDescending(m => m.Description);
                    break;

                case "ReleaseDate":
                    movies = ascending == true ? movies.OrderBy(x => x.ReleaseDate) : movies.OrderByDescending(x => x.ReleaseDate);
                    break;


                default:
                    movies = ascending == true ? movies.OrderBy(m => m.Id) : movies.OrderByDescending(m => m.Id);
                    break;
            }

            return movies.Include(x => x.Director).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


        }



    }
}
