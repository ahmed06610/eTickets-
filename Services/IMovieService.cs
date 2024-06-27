using eTickets.Models;
using eTickets.Services.Base;
using eTickets.ViewModels;

namespace eTickets.Services
{
    public interface IMovieService:IGenericService<Movie>
    {
        Task<Movie> GetMovieById(int id);

        Task<Movie> UpdateMovie(UpdateMovieViewModel data);
/*        Task<List<Movie>> GetAllIn();
*/        public  Task<Movie> AddNewMovie(CreateMovieViewModel data);
    }
}
