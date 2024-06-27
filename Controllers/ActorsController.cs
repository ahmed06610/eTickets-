using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Services;
using eTickets.Services.Base;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorService _actorService;
        private readonly IImageService _imageService;

        public ActorsController(IActorService actorService, IImageService imageService)
        {
            _actorService = actorService;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
            var x = await _actorService.GetAll();
            return View(x);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var model = new CreateProfileViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var m=_imageService.bindingCreateForActor(model);
          await  _actorService.Create(m);
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model=await _actorService.Get(id);
            if (model == null) return View("Empty");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model =await _actorService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var m =await _imageService.bindingUpdateForActor(model);
           await _actorService.Update(m.Id,m);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _actorService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

    }
}
