using AutoMapper;
using DonVo.Services.Interfaces.Directories;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.ViewModels;
using DonVo.ViewModels.DTOs.Directories;
using DonVo.ViewModels.Enums;
using DonVo.WebMVC.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Directories
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _services;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMainDirectories _dropDownList;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public CurrencyController(ICurrencyService services, ISystemAuditService systemAuditService,
                                    IMainDirectories dropDownList, IMapper mapper, IHttpContextAccessor accessor)
        {
            _services = services;
            _systemAuditService = systemAuditService;
            _dropDownList = dropDownList;
            _mapper = mapper;
            _accessor = accessor;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAllAsync();
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Details([Required] string id)
        {
            var result = await _services.GetCurrency(Convert.ToInt32(id));
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(CreateMainCurrencyViewModel vm)
        {
            if (ModelState.IsValid)
            {                
                var result = await _services.CreateMainCurrencyAsync(_mapper.Map<CreateCurrencyDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.DimCurrency);
                    return RedirectToAction("Index");
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(Convert.ToInt32(id));
            if (result.IsValid)
            {
            }
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
