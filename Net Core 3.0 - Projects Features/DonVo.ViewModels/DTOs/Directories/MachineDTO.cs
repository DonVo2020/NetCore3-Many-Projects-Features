using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class MachineDTO
    {
        public int MachineKey { get; set; }
        public string MachineLabel { get; set; }
        public int StoreKey { get; set; }
        public string MachineType { get; set; }
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public string VendorName { get; set; }
        public string MachineOs { get; set; }
        public string MachineSource { get; set; }
        public string MachineHardware { get; set; }
        public string MachineSoftware { get; set; }
        public string Status { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime? DecommissionDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateMachineDTO
    {
        public string MachineLabel { get; set; }
        public int StoreKey { get; set; }
        public string MachineType { get; set; }
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public string VendorName { get; set; }
        public string MachineOs { get; set; }
        public string MachineSource { get; set; }
        public string MachineHardware { get; set; }
        public string MachineSoftware { get; set; }
        public string Status { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime? DecommissionDate { get; set; }
    }
}
