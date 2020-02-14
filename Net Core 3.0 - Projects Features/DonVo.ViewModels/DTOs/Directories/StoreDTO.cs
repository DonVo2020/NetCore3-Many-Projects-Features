using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class StoreDTO
    {
        public int StoreKey { get; set; }
        public int GeographyKey { get; set; }
        public int? StoreManager { get; set; }
        public string StoreType { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? EntityKey { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodeExtension { get; set; }
        public string StorePhone { get; set; }
        public string StoreFax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CloseReason { get; set; }
        public int? EmployeeCount { get; set; }
        public double? SellingAreaSize { get; set; }
        public DateTime? LastRemodelDate { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateStoreDTO
    {
        public int GeographyKey { get; set; }
        public int? StoreManager { get; set; }
        public string StoreType { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodeExtension { get; set; }
        public string StorePhone { get; set; }
        public string AddressLine1 { get; set; }
        public string CloseReason { get; set; }
        public int? EmployeeCount { get; set; }
        public double? SellingAreaSize { get; set; }
        public DateTime? LastRemodelDate { get; set; }
    }
}
