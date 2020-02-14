using AutoMapper;
using DonVo.Services.Interfaces.Directories;
using DonVo.Services.Interfaces.SystemAudit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Directories
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _services;
        private readonly IMainDirectories _dropDownList;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISystemAuditService _systemAuditService;

        public ProductCategoryController(IProductCategoryService services, IMainDirectories dropDownList, IMapper mapper, IHttpContextAccessor accessor, ISystemAuditService systemAuditService)
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

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
