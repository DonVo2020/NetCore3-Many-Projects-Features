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
    public class ProductSubcategoryController : Controller
    {
        private readonly IProductSubcategoryService _services;
        private readonly IMainDirectories _dropDownList;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISystemAuditService _systemAuditService;

        public ProductSubcategoryController(IProductSubcategoryService services, IMainDirectories dropDownList, IMapper mapper, IHttpContextAccessor accessor, ISystemAuditService systemAuditService)
        {
            _services = services;
            _dropDownList = dropDownList;
            _mapper = mapper;
            _accessor = accessor;
            _systemAuditService = systemAuditService;
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
            var result = await _services.GetProductCategory(Convert.ToInt32(id));
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CreateAsync()
        {
            await FillingDropDownLists();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(CreateMainProductSubcategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateMainProductSubcategoryAsync(_mapper.Map<CreateProductSubcategoryDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.DimProductSubcategory);
                    return RedirectToAction("Index");
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateProductSubcategory([Required] int id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                await FillingDropDownLists();
                return View(_mapper.Map<UpdateProductSubcategoryViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdateProductSubcategory")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductSubcategoryConfirmed(UpdateProductSubcategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateProductSubcategoryAsync(_mapper.Map<UpdateProductSubcategoryDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.DimProductSubcategory);
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
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.DimProductSubcategory);
            }
            return Ok(result);
        }

        private async Task FillingDropDownLists()
        {
            ViewBag.ProductCategory = await _dropDownList.GetProductCategory();
        }

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
