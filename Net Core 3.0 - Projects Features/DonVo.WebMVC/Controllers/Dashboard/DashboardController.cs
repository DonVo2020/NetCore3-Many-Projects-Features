﻿using AutoMapper;
using DonVo.Services.Interfaces.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonVo.WebMVC.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _services;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult PieData()
        {
            return Json(_services.PieData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BarData()
        {
            return Json(_services.BarData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AreaData()
        {
            return Json(_services.AreaData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RadarData()
        {
            return Json(_services.RadarData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult LineData()
        {
            return Json(_services.LineData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BubbleData()
        {
            return Json(_services.BubbleData_Test());
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}