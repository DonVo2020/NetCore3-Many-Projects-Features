using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs;
using DonVo.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.SystemAudit
{
    public interface ISystemAuditService : IDisposable
    {
        Task AuditAsync(string email, string ipUser, Operations operation, Tables table, string columnName = null);
        Task AuditAsync(string email, string ipUser, Operations operation, Tables[] table);
        Task<ActualResult<IEnumerable<SystemAuditDTO>>> FilterAsync(string email, DateTime startDate, DateTime endDate);
    }
}
