using DonVo.SystemAudit.AuditModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.SystemAudit.Repository
{
    public interface ISystemAuditRepository : IDisposable
    {
        Task AuditAsync(AuditContosoRetailDw systemAudit);
        Task<IEnumerable<AuditContosoRetailDw>> FilterAsync(string email, DateTime startDate, DateTime endDate);
    }

    public class SystemAuditRepository : ISystemAuditRepository
    {
        private readonly DonVoSystemAuditContext _dbContext;

        public SystemAuditRepository(DonVoSystemAuditContext db)
        {
            _dbContext = db;
        }

        public async Task AuditAsync(AuditContosoRetailDw auditContosoRetailDw)
        {
            await _dbContext.AddAsync(auditContosoRetailDw);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditContosoRetailDw>> FilterAsync(string email, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.AuditContosoRetailDW
                        .Where(x => x.EmailUser == email && x.CreatedOn >= startDate && x.CreatedOn <= endDate)
                        .OrderByDescending(o=>o.CreatedOn)
                        .ToListAsync();
        }       

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}