using AutoMapper;
using DonVo.Services.Directories;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.ViewModels;
using DonVo.ViewModels.DTOs.Directories;
using DonVo.ViewModels.Enums;
using DonVo.WebMVC.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Directories
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _services;
        private readonly IMainDirectories _dropDownList;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISystemAuditService _systemAuditService;

        public CustomerController(ICustomerService services, IMainDirectories dropDownList, IMapper mapper, IHttpContextAccessor accessor, ISystemAuditService systemAuditService)
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
            var result = await _services.GetCustomer(id);
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
        public async Task<IActionResult> CreateConfirmed(CreateMainCustomerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateMainCustomerAsync(_mapper.Map<CreateCustomerDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.DimCustomer);
                    return RedirectToAction("Index");
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult CreateCustomer([Required] string id)
        {
            return View(new CreateCustomerCustomerViewModel { HashIdMain = id });
        }

        [HttpPost, ActionName("CreateCustomer")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomerConfirmed(CreateCustomerCustomerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateCustomerCustomerAsync(_mapper.Map<CreateCustomerCustomerDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.DimCustomer);
                    return RedirectToAction("Details", new { id = vm.CustomerKey });
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateCustomer([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateCustomerViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdateCustomer")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomerConfirmed(UpdateCustomerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateCustomerAsync(_mapper.Map<CustomerDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.DimCustomer);
                    return RedirectToAction("Details", new { id = vm.CustomerKey });
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateEmailAddress([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateCustomerEmailAddressViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdateEmailAddress")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmailAddressConfirmed(UpdateCustomerEmailAddressViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var customer = await _services.GetAsync(vm.CustomerKey.ToString());
                customer.Result.EmailAddress = vm.EmailAddress;
                var result = await _services.UpdateCustomerEmailAddressAsync(customer.Result);
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.DimCustomer, "EmailAddress");
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateCompanyName([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateCustomerCompanyNameViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdateCompanyName")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCompanyNameConfirmed(UpdateCustomerCompanyNameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var customer = await _services.GetAsync(vm.CustomerKey.ToString());
                customer.Result.CompanyName = vm.CompanyName;
                var result = await _services.UpdateCustomerCompanyNameAsync(customer.Result);
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.DimCustomer, "CompanyName");
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateEducation([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateCustomerEducationViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdateEducation")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEducationConfirmed(UpdateCustomerEducationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateCustomerEducationAsync(_mapper.Map<UpdateCustomerEducationDTO>(vm));
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Restructuring([Required] string id)
        {
            var customerForMvc = await _services.GetCustomerForMvc(id);
            ViewBag.MainCustomer = await _dropDownList.GetMainCustomer();
            ViewBag.CustomerCustomer = new SelectList(customerForMvc, "Key", "Value");
            return View();
        }

        [HttpPost, ActionName("Restructuring")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestructuringConfirmed(RestructuringViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.RestructuringCustomer(_mapper.Map<RestructuringCustomerDTO>(vm));
                if (result.IsValid)
                {
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
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.DimCustomer);
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