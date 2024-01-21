using ExamTask.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPortfolioService _service;

        public HomeController(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _service.GetAllAsync();

            return View(entities);
        }

        public IActionResult AccessDeniedCustom()
        {
            return View();
        }
    }
}
