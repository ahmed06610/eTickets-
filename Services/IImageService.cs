using eTickets.Models;
using eTickets.ViewModels;

namespace eTickets.Services
{
    public interface IImageService
    {
        //Actor
        Actor bindingCreateForActor(CreateProfileViewModel viewmodel);
        Task<Actor> bindingUpdateForActor(UpdateProfileViewModel viewmodel);

        //Producer
        Producer bindingCreateForProducer(CreateProfileViewModel viewmodel);
        Task<Producer> bindingUpdateForProducer(UpdateProfileViewModel viewmodel);
        
        //Cinema
        Cinema bindingCreateForCinema(CreateCinemaViewModel viewmodel);
        Task<Cinema> bindingUpdateForCinema(UpdateCinemaViewModel viewmodel);


        //Movie
        Movie bindingCreateForMovie(CreateMovieViewModel viewmodel);
        Task<Movie> bindingUpdateForMovie(UpdateMovieViewModel viewmodel);


        string SaveCover(IFormFile cover);
    }
}
