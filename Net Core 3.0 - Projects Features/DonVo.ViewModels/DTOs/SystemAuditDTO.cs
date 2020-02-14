using DonVo.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.ViewModels.DTOs
{
    public class SystemAuditDTO
    {
        public Guid Guid { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public int? ColumnId { get; set; }
        public string ColumnName { get; set; }
        public string IpUser { get; set; }
        public string EmailUser { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
