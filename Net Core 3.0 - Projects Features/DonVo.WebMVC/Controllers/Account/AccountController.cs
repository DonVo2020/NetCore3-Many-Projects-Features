using AutoMapper;
using DonVo.Services.Interfaces.Account;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.ViewModels;
using DonVo.ViewModels.DTOs;
using DonVo.ViewModels.Enums;
using DonVo.WebMVC.Controllers.Directories;
using DonVo.WebMVC.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMainDirectories _dropDownList;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public AccountController(IAccountService accountService, ISystemAuditService systemAuditService, 
                                    IMainDirectories dropDownList, IMapper mapper, IHttpContextAccessor accessor)
        {
            _accountService = accountService;
            _systemAuditService = systemAuditService;
            _dropDownList = dropDownList;
            _mapper = mapper;
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountService.SignIn(model.Email, model.Password, model.RememberMe))
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Directory", "Home");
                }
                ViewBag.IncorectLoginPassword = "Wrong Username or Password!";
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SignOut()
        {
            await _accountService.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await _accountService.GetAllAccountsAsync();
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Role = await _dropDownList.GetRoles();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(CreateAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateAsync(_mapper.Map<CreateAccountDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.AspNetUsers);
                    return RedirectToAction("Index");
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePersonalData([Required] string id)
        {
            var result = await _accountService.GetAccountAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdatePersonalDataAccountViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdatePersonalData")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePersonalDataConfirmed(UpdatePersonalDataAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdatePersonalDataAsync(_mapper.Map<AccountDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.AspNetUsers);
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmail([Required] string id)
        {
            var result = await _accountService.GetAccountAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateEmailAccountViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }


        [HttpPost, ActionName("UpdateEmail")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmailConfirmed(UpdateEmailAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateEmailAsync(_mapper.Map<AccountDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.AspNetUsers, "Email");
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePassword([Required] string id)
        {
            return View(new UpdatePasswordAccountViewModel { HashId = id });
        }

        [HttpPost, ActionName("UpdatePassword")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePasswordConfirmed(UpdatePasswordAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdatePasswordAsync(_mapper.Map<UpdateAccountPasswordDTO>(vm));
                if(result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.AspNetUsers, "PasswordHash");
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([Required] string id)
        {
            var result = await _accountService.GetAccountRoleAsync(id);
            if (result.IsValid)
            {
                ViewBag.Role = await _dropDownList.GetRoles();
                return View(_mapper.Map<UpdateRoleAccountViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }


        [HttpPost, ActionName("UpdateRole")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoleConfirmed(UpdateRoleAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateRoleAsync(_mapper.Map<AccountDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.AspNetUserRoles, "RoleId");
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.AspNetUsers);
            return Ok(await _accountService.DeleteAsync(id));
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckEmail([Required] string email)
        {
            return Json(!await _accountService.CheckEmailAsync(email));
        }

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}