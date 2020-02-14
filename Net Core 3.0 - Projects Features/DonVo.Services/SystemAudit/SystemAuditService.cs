using DonVo.Services.ActualResults;
using DonVo.Services.Helpers;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.SystemAudit.AuditModels;
using DonVo.SystemAudit.Repository;
using DonVo.ViewModels.DTOs;
using DonVo.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.Services.SystemAudit
{
    public class SystemAuditService : ISystemAuditService
    {
        private readonly ISystemAuditRepository _auditRepository;
        public SystemAuditService(ISystemAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task AuditAsync(string email, string ipUser, Operations operation, Tables table, string columnName = null)
        {
            try
            {
                await _auditRepository.AuditAsync(
                        new AuditContosoRetailDw
                        {
                            EmailUser = email,
                            IpUser = ipUser,
                            OperationId = (int)operation,
                            OperationName = operation.ToString(),
                            TableId = (int)table,
                            TableName = table.ToString(),
                            ColumnName = columnName,
                            CreatedOn = DateTime.Now
                        });
            }
            catch (Exception exception)
            {
                new ActualResult<AuditContosoRetailDw>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task AuditAsync(string email, string ipUser, Operations operation, Tables[] tables)
        {
            try
            {
                foreach (var table in tables)
                {
                    await AuditAsync(email, ipUser, operation, table);
                }
            }
            catch (Exception exception)
            {
                new ActualResult<AuditContosoRetailDw>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<SystemAuditDTO>>> FilterAsync(string email, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _auditRepository.FilterAsync(email, startDate, endDate);
                return new ActualResult<IEnumerable<SystemAuditDTO>>
                {
                    Result = result.Select(audit => new SystemAuditDTO
                    {
                        EmailUser = audit.EmailUser,
                        CreatedOn = audit.CreatedOn,
                        OperationName = audit.OperationName,
                        TableName = audit.TableName,
                        ColumnName = audit.ColumnName
                    }).ToList()
                };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<SystemAuditDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _auditRepository.Dispose();
        }
    }
}