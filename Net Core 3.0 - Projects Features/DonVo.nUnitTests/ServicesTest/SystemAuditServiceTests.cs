using DonVo.Services.Interfaces.SystemAudit;
using DonVo.Services.SystemAudit;
using DonVo.SystemAudit.AuditModels;
using DonVo.ViewModels.DTOs;
using DonVo.ViewModels.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.nUnitTests.ServicesTest
{
    public class SystemAuditServiceTests : TestsBase
    {
        ISystemAuditService _service;

        [SetUp]
        public void Setup()
        {
            _service = new SystemAuditService(_mockedRepository.Object);
        }

        [Test]
        public async Task GetAuditContosoRetailDwList_ShouldBeCallFilterAsyncMethod()
        {
            string email = "right.admin@test.com";
            DateTime startDate = DateTime.Now.AddDays(-10);
            DateTime endDate = DateTime.Now.AddDays(1);

            _mockedRepository.Setup(x => x.FilterAsync(It.IsAny<string>(), startDate, endDate)).ReturnsAsync(new List<AuditContosoRetailDw>());

            var result = await _service.FilterAsync(email, startDate, endDate);

            _mockedRepository.Verify(x => x.FilterAsync(It.IsAny<string>(), startDate, endDate), Times.Exactly(1));
        }

        [Test]
        public async Task AddAudit_ShouldCallAuditAsyncWithRightArguments()
        {
            string email = "nUnitTests@donvo.edu";
            string ipUser = "127.0.0.1";
            Operations operation = Operations.Insert;
            Tables table = Tables.AuditContosoRetailDw;

            try
            {
                _mockedRepository.Setup(x => x.AuditAsync(It.IsAny<AuditContosoRetailDw>()));

                await _service.AuditAsync(email, ipUser, operation, table);

                _mockedRepository.Verify(x => x.AuditAsync(It.Is<AuditContosoRetailDw>(
                            t =>    t.EmailUser == "nUnitTests@donvo.edu" &&
                                    t.TableId == 10000 &&
                                    t.IpUser == "127.0.0.1" &&
                                    t.OperationName == Operations.Insert.ToString() &&
                                    t.TableName == Tables.AuditContosoRetailDw.ToString())));

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
