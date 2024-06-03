using SOA2024.MovieReview.API.ExternalModels;

namespace UshtrimeKOL2.Services
{
    public interface ITVDBServise
    {
        Task<List<TVDBMovie>> GetTopRatedMoviesAsync();
    }
}
