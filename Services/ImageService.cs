
using eTickets.Data;
using eTickets.Data.Enums;
using eTickets.Models;
using eTickets.Settings;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eTickets.Services
{
    public class ImageService : IImageService
    {
        private readonly appdbcontext _context;
        private readonly IProducerService _producerService;
        private readonly IActorService _actorService;
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;
        private readonly IWebHostEnvironment _webHost;
        private string _imagePath;

        public ImageService(appdbcontext context, IProducerService producerService, IActorService actorService, ICinemaService cinemaService, IWebHostEnvironment webHost, IMovieService movieService)
        {
            _context = context;
            _producerService = producerService;
            _actorService = actorService;
            _cinemaService = cinemaService;
            _webHost = webHost;
            _movieService = movieService;
        }

        public Actor bindingCreateForActor(CreateProfileViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Actors";

            var m = viewmodel;

            var coverName = SaveCover(m.ProfileImage);
            Actor actor = new Actor
            {
                FullName = m.FullName,
                Bio = m.Bio,
                ProfileImage = coverName,
                Actors_Movies = null,

            };
            return actor;

        }

        public Cinema bindingCreateForCinema(CreateCinemaViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Cinemas";

            var m = viewmodel;

            var coverName = SaveCover(m.LogoImage);
            Cinema Cinema = new Cinema
            {
                Name = m.Name,
                Description = m.Description,
                LogoImage = coverName,


            };
            return Cinema;
        }

        public Movie bindingCreateForMovie(CreateMovieViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Movies";

            var m = viewmodel;

            var coverName = SaveCover(m.MovieImage);
           
            var newMovie = new Movie()
            {
                Name = viewmodel.Name,
                Description = viewmodel.Description,
                Price = viewmodel.Price,
                MovieImage = coverName,
                CinemaId = viewmodel.CinemaId,
                StartDate = viewmodel.StartDate,
                EndDate = viewmodel.EndDate,
                MovieCategory = viewmodel.MovieCategory,
                ProducerId = viewmodel.ProducerId,
                

            };
            foreach (var actorId in viewmodel.ActorIds)
            {
                var newActorMovie = new Actor_movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                 _context.Actors_Movies.Add(newActorMovie);
            }
             _context.SaveChanges();
            return newMovie;
        }

        public Producer bindingCreateForProducer(CreateProfileViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Producers";

            var m = viewmodel;

            var coverName = SaveCover(m.ProfileImage);
            Producer Producer = new Producer
            {
                FullName = m.FullName,
                Bio = m.Bio,
                ProfileImage = coverName,

            };
            return Producer;
        }

        ////////////////////////////////////////////////////////////////////////////////

        public async Task<Actor> bindingUpdateForActor(UpdateProfileViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Actors";

            var m = viewmodel;

            var actor = await _actorService.Get(m.Id);
            if (actor is null) return null;

            var hasNewImage = m.ProfileImage != null;
            var oldImage = actor.ProfileImage;

            actor.FullName = m.FullName;
            actor.Bio = m.Bio;


            if (hasNewImage)
            {
                actor.ProfileImage = SaveCover(m.ProfileImage!);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagePath, oldImage);
                    File.Delete(cover);
                }
                return actor;
            }


            return actor;
        }

        public async Task<Cinema> bindingUpdateForCinema(UpdateCinemaViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Cinemas";

            var m = viewmodel;

            var cinema = await _cinemaService.Get(m.Id);
            if (cinema is null) return null;

            var hasNewImage = m.LogoImage != null;
            var oldImage = cinema.LogoImage;

            cinema.Name = m.Name;
            cinema.Description = m.Description;


            if (hasNewImage)
            {
                cinema.LogoImage = SaveCover(m.LogoImage!);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagePath, oldImage);
                    File.Delete(cover);
                }
                return cinema;
            }
            return cinema;
        }

        public async Task<Movie> bindingUpdateForMovie(UpdateMovieViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Movies";

            var hasNewImage = viewmodel.MovieImage != null;

            var dbMovie = await _movieService.Get(viewmodel.Id);
            if (dbMovie is null)  return null; 
            var oldImage = dbMovie.MovieImage;
            
                dbMovie.Name = viewmodel.Name;
                dbMovie.Description = viewmodel.Description;
                dbMovie.Price = viewmodel.Price;
                dbMovie.CinemaId = viewmodel.CinemaId;
                dbMovie.StartDate = viewmodel.StartDate;
                dbMovie.EndDate = viewmodel.EndDate;
                dbMovie.MovieCategory = viewmodel.MovieCategory;
                dbMovie.ProducerId = viewmodel.ProducerId;
            



            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == viewmodel.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
             _context.SaveChanges();

            //Add Movie Actors
            foreach (var actorId in viewmodel.ActorIds)
            {
                var newActorMovie = new Actor_movie()
                {
                    MovieId = viewmodel.Id,
                    ActorId = actorId
                };
                 _context.Actors_Movies.Add(newActorMovie);
            }

            //Check for the image 
            if (hasNewImage)
            {
                dbMovie.MovieImage = SaveCover(viewmodel.MovieImage!);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagePath, oldImage);
                    File.Delete(cover);
                }
                return dbMovie;
            }
            return dbMovie;
        }

        public async Task<Producer> bindingUpdateForProducer(UpdateProfileViewModel viewmodel)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Producers";

            var m = viewmodel;

            var producer = await _producerService.Get(m.Id);
            if (producer is null) return null;

            var hasNewImage = m.ProfileImage != null;
            var oldImage = producer.ProfileImage;

            producer.FullName = m.FullName;
            producer.Bio = m.Bio;


            if (hasNewImage)
            {
                producer.ProfileImage = SaveCover(m.ProfileImage!);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagePath, oldImage);
                    File.Delete(cover);
                }
                return producer;
            }
            return producer;
        }

        public string SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            cover.CopyTo(stream);

            return coverName;

        }
    }
}
