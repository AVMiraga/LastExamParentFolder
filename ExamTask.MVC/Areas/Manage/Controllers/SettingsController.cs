using ExamTask.Business.Services;
using ExamTask.Core.Entities;
using ExamTask.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamTask.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingsController : Controller
    {
        private readonly SettingService _service;
        private readonly AppDbContext _context;

        public SettingsController(SettingService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Update()
        {
            var settings = _service.GetSettings();

            settings.TryAdd("Brand", "");
            settings.TryAdd("Twitter", "");
            settings.TryAdd("Facebook", "");
            settings.TryAdd("Linkedin", "");

            return View(settings);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult Update(Dictionary<string, string> updatedSettings)
        {
            foreach (var item in updatedSettings)
            {
                if (item.Key == "__RequestVerificationToken") continue;

                var setting = _context.Settings.Where(x => x.Key == item.Key).FirstOrDefault();

                setting.Value = item.Value;

                _context.Update(setting);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
