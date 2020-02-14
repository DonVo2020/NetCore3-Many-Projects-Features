using AutoMapper;
using DonVo.Services.Interfaces.Directories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Directories
{
    public class ProductController : Controller
    {
        private readonly IProductService _services;
        private readonly IMainDirectories _dropDownList;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ProductController(IProductService services, IMainDirectories dropDownList, IMapper mapper, IHttpContextAccessor accessor)
        {
            _services = services;
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
            var result = await _services.GetProduct(Convert.ToInt32(id));
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
