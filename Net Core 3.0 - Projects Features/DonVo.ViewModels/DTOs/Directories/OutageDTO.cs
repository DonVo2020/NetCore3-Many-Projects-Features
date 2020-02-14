using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class OutageDTO
    {
        public int OutageKey { get; set; }
        public string OutageLabel { get; set; }
        public string OutageName { get; set; }
        public string OutageDescription { get; set; }
        public string OutageType { get; set; }
        public string OutageTypeDescription { get; set; }
        public string OutageSubType { get; set; }
        public string OutageSubTypeDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateOutageDTO
    {
        public string OutageLabel { get; set; }
        public string OutageName { get; set; }
        public string OutageDescription { get; set; }
        public string OutageType { get; set; }
        public string OutageTypeDescription { get; set; }
        public string OutageSubType { get; set; }
        public string OutageSubTypeDescription { get; set; }
    }
}
