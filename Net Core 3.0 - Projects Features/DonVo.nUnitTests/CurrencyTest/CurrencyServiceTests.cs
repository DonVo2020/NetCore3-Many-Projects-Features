using AutoMapper;
using DonVo.Services.ActualResults;
using DonVo.Services.Directories;
using DonVo.Services.Interfaces.Directories;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.Services.SystemAudit;
using DonVo.SystemAudit.AuditModels;
using DonVo.ViewModels.DTOs;
using DonVo.ViewModels.DTOs.Directories;
using DonVo.ViewModels.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.nUnitTests.ServicesTest
{
    public class CurrencyServiceTests : TestsBase
    {
        ICurrencyService _service;

        [SetUp]
        public void Setup()
        {
            _service = new CurrencyService(_context, _autoMapper.Create());
        }

        [Test]
        public async Task UpdateCurrency_ShouldBeCallUpdateMethodWithRightArguments()
        {
            try
            {
                var currencyDTO = new CreateCurrencyDTO();
                currencyDTO.CurrencyName = "Unit Test";
                currencyDTO.CurrencyDescription = "Unit Test Description";
                currencyDTO.CurrencyLabel = "444";
                currencyDTO.CurrencyKey = 9999999;

                //var currencyDTO2 = new ActualResult<CreateCurrencyDTO>();
                //currencyDTO2.Result.CurrencyName = "Unit Test";
                //currencyDTO2.Result.CurrencyDescription = "Unit Test Description";
                //currencyDTO2.Result.CurrencyLabel = "444";
                //currencyDTO2.Result.CurrencyKey = 2;

                //var _mapper = _autoMapper.Create();

                //_.Setup(x => x.Map<CreateCurrencyDTO>(It.IsAny<Persistences.Models.DimCurrency>()));

                //var test = _currencyService.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(currencyDTO);
                _currencyService.Setup(x => x.UpdateAsync(It.IsAny<CreateCurrencyDTO>())).ReturnsAsync((ActualResult x) => x);

                await _service.UpdateAsync(currencyDTO);

                //_currencyService.Verify(x => x.UpdateAsync(
                //        It.Is<CreateCurrencyDTO>(c => c.CurrencyKey == currencyDTO.CurrencyKey &&  c.CurrencyName == currencyDTO.CurrencyName && c.CurrencyDescription == currencyDTO.CurrencyName && c.CurrencyLabel == currencyDTO.CurrencyLabel)),
                //    Times.Exactly(1));

                _currencyService.Verify(x => x.UpdateAsync(It.Is<CreateCurrencyDTO>(
                            t => t.CurrencyKey == currencyDTO.CurrencyKey &&
                                    t.CurrencyName == currencyDTO.CurrencyName &&
                                    t.CurrencyDescription == currencyDTO.CurrencyDescription &&
                                    t.CurrencyLabel == currencyDTO.CurrencyLabel)));

                //_currencyService.Verify(r => r.UpdateAsync(It.IsAny<CreateCurrencyDTO>()), Times.Once);

                Assert.IsTrue(true);
            }
            catch(Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}
