using eTickets.Data;
using eTickets.Data.Enums;
using eTickets.Models;
using eTickets.Services.Base;
using eTickets.Settings;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Services
{
    public class MovieService : GenericService<Movie>, IMovieService
    {


        /*public MovieService(appdbcontext context) : base(context)
        {
        }*/


        private readonly appdbcontext _context;
        private readonly IWebHostEnvironment _webHost;

        private string _imagePath;

        public MovieService(appdbcontext context, IWebHostEnvironment webHost):base(context)
        {
            _context = context;
            _webHost = webHost;
        }

        public async Task<Movie> AddNewMovie(CreateMovieViewModel data)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Movies";
            var coverName = SaveCover(data.MovieImage);

            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                MovieImage = coverName,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();

            return newMovie;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movieDetails = await _context.Movies
               .Include(c => c.Cinema)
               .Include(p => p.Producer)
               .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
               .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<Movie> UpdateMovie(UpdateMovieViewModel data)
        {
            _imagePath = $"{_webHost.WebRootPath}{FileSettings.ImagesPath}/Movies";

            var hasNewImage = data.MovieImage != null;

            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            var oldImage = dbMovie.MovieImage;
            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
            }



            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }



            //Check for the image 
            if (hasNewImage)
            {
                dbMovie.MovieImage = SaveCover(data.MovieImage!);
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

        public string SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            cover.CopyTo(stream);

            return coverName;

        }

        /*  public async Task<List<Movie>> GetAllIn()
          {
             return await _context.Movies.Include(n => n.Cinema).ToListAsync();
          }*/

    }
}
