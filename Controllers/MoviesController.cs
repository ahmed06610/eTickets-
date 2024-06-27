
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Services;
using eTickets.Services.Base;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;
        private readonly IProducerService _producerService;
        private readonly IActorService _actorService;
        private readonly IImageService _imageService;

        public MoviesController(IMovieService movieService, ICinemaService cinemaService, IProducerService producerService,
            IActorService actorService, IImageService imageService)
        {
            _movieService = movieService;
            _cinemaService = cinemaService;
            _producerService = producerService;
            _actorService = actorService;
            _imageService = imageService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _movieService.GetAll(n => n.Cinema);
            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateMovieViewModel();

            ViewBag.Cinemas = new SelectList(await _cinemaService.GetAll(), "Id", "Name");
            ViewBag.Actors = new SelectList(await _actorService.GetAll(), "Id", "FullName");
            ViewBag.Producers = new SelectList(await _producerService.GetAll(), "Id", "FullName");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Cinemas = new SelectList( await _cinemaService.GetAll(), "Id", "Name");
                ViewBag.Actors = new SelectList(await _actorService.GetAll(), "Id", "FullName");
                ViewBag.Producers = new SelectList(await _producerService.GetAll(), "Id", "FullName");
                return View(model);
            }
          await  _movieService.AddNewMovie(model);

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _movieService.GetAll(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                //var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
                var filteredResultNew=allMovies.Where(n=>n.Name.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResultNew);
            }

            return View("Index", allMovies);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var movieDetails = await _movieService.GetMovieById(id);
            

            if (movieDetails != null)
            {
                var response = new UpdateMovieViewModel();



                response.Id = movieDetails.Id;
                response.Name = movieDetails.Name;
                response.Description = movieDetails.Description;
                response.Price = movieDetails.Price;
                response.StartDate = movieDetails.StartDate;
                response.EndDate = movieDetails.EndDate;
                response.MovieCategory = movieDetails.MovieCategory;
                response.CinemaId = movieDetails.CinemaId;
                response.ProducerId = movieDetails.ProducerId;
                response.ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList();
                response.img = movieDetails.MovieImage;
                


                ViewBag.Cinemas = new SelectList(await _cinemaService.GetAll(), "Id", "Name");
                ViewBag.Actors = new SelectList(await _actorService.GetAll(), "Id", "FullName");
                ViewBag.Producers = new SelectList(await _producerService.GetAll(), "Id", "FullName");

                return View(response);
            }
            else
                { return View("NOT Found"); }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(await _cinemaService.GetAll(), "Id", "Name");
                ViewBag.Actors = new SelectList(await _actorService.GetAll(), "Id", "FullName");
                ViewBag.Producers = new SelectList(await _producerService.GetAll(), "Id", "FullName");
                
                return View(model);
            }
            await _movieService.UpdateMovie(model);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _movieService.GetMovieById(id);
            if (model == null) return View("Empty");
            return View(model);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _movieService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
