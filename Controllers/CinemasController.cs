using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Services;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;
        private readonly IImageService _imageService;

        public CinemasController(ICinemaService cinemaService, IImageService imageService)
        {
            _cinemaService = cinemaService;
            _imageService = imageService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _cinemaService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateCinemaViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCinemaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var m = _imageService.bindingCreateForCinema(model);
            _cinemaService.Create(m);
            return Redirect(nameof(Index));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _cinemaService.Get(id);

            if (model == null) return View("Empty");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _cinemaService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCinemaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var m = await _imageService.bindingUpdateForCinema(model);
            await _cinemaService.Update(m.Id, m);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _cinemaService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
