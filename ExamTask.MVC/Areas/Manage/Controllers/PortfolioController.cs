using AutoMapper;
using ExamTask.Business.Services.Interfaces;
using ExamTask.Business.ViewModels.EntityVms;
using ExamTask.Core.Entities;
using ExamTask.DAL.Context;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPortfolioService _service;
        private readonly AppDbContext _context;

        public PortfolioController(IMapper mapper, IPortfolioService service, AppDbContext context)
        {
            _mapper = mapper;
            _service = service;
            _context = context;
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Admin"))
            {
                var entities = _context.Portfolios;
                return View(entities);
            }
            else
            {
                var entities = await _service.GetAllAsync();
                return View(entities);
            }

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePortfolioVm vm)
        {
            CreatePortfolioVmValidator validator = new CreatePortfolioVmValidator();
            ValidationResult result = await validator.ValidateAsync(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View();
            }

            await _service.CreateAsync(vm);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            Portfolio portfolio = await _service.GetByIdAsync(id);

            if(portfolio == null)
            {
                return RedirectToAction("Index");
            }

            UpdatePortfolioVm vm = _mapper.Map<UpdatePortfolioVm>(portfolio);

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePortfolioVm vm)
        {
            UpdatePortfolioVmValidator validator = new UpdatePortfolioVmValidator();
            ValidationResult result = await validator.ValidateAsync(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View();
            }

            await _service.UpdateAsync(vm);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_service.GetByIdAsync(id) != null)
                return RedirectToAction("Index");

            await _service.DeleteAsync(id);

            return RedirectToAction("Index");
        }

    }
}
