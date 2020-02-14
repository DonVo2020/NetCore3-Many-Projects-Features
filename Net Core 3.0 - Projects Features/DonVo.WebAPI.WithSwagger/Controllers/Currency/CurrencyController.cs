using AutoMapper;
using DonVo.Services.Interfaces.Directories;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.ViewModels;
using DonVo.ViewModels.DTOs.Directories;
using DonVo.ViewModels.Enums;
using DonVo.WebAPI.WithSwagger.Attributes;
using DonVo.WebAPI.WithSwagger.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonVo.WebAPI.WithSwagger.Controllers.Currency
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _services;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ICurrencyService services, ISystemAuditService systemAuditService, IMapper mapper, IHttpContextAccessor accessor, ILogger<CurrencyController> logger)
        {
            _services = services;
            _systemAuditService = systemAuditService;
            _mapper = mapper;
            _accessor = accessor;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAllAsync();
            if (result.IsValid)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorsList);
        }

        [HttpGet]
        [Route("Get/{id}", Name = "GetCurrency")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CurrencyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([Required] string id)
        {
            var result = await _services.GetAsync(Convert.ToInt32(id));
            if (result.IsValid)
            {
                return Ok(result.Result);
            }
            return NotFound(result.ErrorsList);
        }

        [HttpPost]
        [Route("Create")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CreateMainCurrencyViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreateMainCurrencyViewModel vm)
        {
            var result = await _services.CreateMainCurrencyAsync(_mapper.Map<CreateCurrencyDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.DimCurrency);
                return CreatedAtRoute("GetCurrency", new { version = "1.0", controller = "Currency", id = result.Result }, vm);
            }
            return UnprocessableEntity(result.ErrorsList);
        }

        [HttpPut]
        [Route("Update")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] CreateMainCurrencyViewModel vm)
        {
            var result = await _services.UpdateAsync(_mapper.Map<CreateCurrencyDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.DimCurrency);
                return NoContent();
            }
            return BadRequest(result.ErrorsList);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(Convert.ToInt32(id));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.DimCurrency);
                return NoContent();
            }
            return NotFound(result.ErrorsList);
        }
    }
}