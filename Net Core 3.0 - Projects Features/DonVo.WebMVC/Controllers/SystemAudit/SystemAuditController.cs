using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.WebMVC.Controllers.Directories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonVo.WebMVC.Controllers.SystemAudit
{
    public class SystemAuditController : Controller
    {
        private readonly IMainDirectories _directories;
        private readonly ISystemAuditService _systemAuditService;

        public SystemAuditController(IMainDirectories directories, ISystemAuditService systemAuditService)
        {
            _directories = directories;
            _systemAuditService = systemAuditService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Emails = await _directories.GetEmails();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Filter([Required] string email, [Required] DateTime startDate, [Required] DateTime endDate)
        {
            var result = await _systemAuditService.FilterAsync(email, startDate, endDate);
            if (result.IsValid)
            {
                ViewBag.Emails = await _directories.GetEmails();
                return View("Index", result.Result);
            }
            return BadRequest();
        }
    }
}
