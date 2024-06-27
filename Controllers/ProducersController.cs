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
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;
        private readonly IImageService _imageService;

        public ProducersController(IProducerService producerService, IImageService imageService)
        {
            _producerService = producerService;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
           
            return View(await _producerService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateProfileViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var m = _imageService.bindingCreateForProducer(model);
            _producerService.Create(m);
            return Redirect(nameof(Index));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _producerService.Get(id);
            if (model == null) return View("Empty");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _producerService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var m = await _imageService.bindingUpdateForProducer(model);
            await _producerService.Update(m.Id, m);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _producerService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
