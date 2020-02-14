using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    public class CreateMainStoreViewModel
    {
        [Required(ErrorMessage = "StoreKey cannot be blank!")]
        public int StoreKey { get; set; }
        [Required(ErrorMessage = "GeographyKey cannot be blank!")]
        public int GeographyKey { get; set; }
        public int? StoreManager { get; set; }
        [Required(ErrorMessage = "StoreType cannot be blank!")]
        public string StoreType { get; set; }
        [Required(ErrorMessage = "StoreName cannot be blank!")]
        public string StoreName { get; set; }
        [Required(ErrorMessage = "StoreDescription cannot be blank!")]
        public string StoreDescription { get; set; }
        [Required(ErrorMessage = "Status cannot be blank!")]
        public string Status { get; set; }
        [Required(ErrorMessage = "OpenDate cannot be blank!")]
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        [Required(ErrorMessage = "ZipCode cannot be blank!")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "ZipCodeExtension cannot be blank!")]
        public string ZipCodeExtension { get; set; }
        [Required(ErrorMessage = "StorePhone cannot be blank!")]
        public string StorePhone { get; set; }
        [Required(ErrorMessage = "AddressLine1 cannot be blank!")]
        public string AddressLine1 { get; set; }
        [Required(ErrorMessage = "CloseReason cannot be blank!")]
        public string CloseReason { get; set; }
        public int? EmployeeCount { get; set; }
        public double? SellingAreaSize { get; set; }
        public DateTime? LastRemodelDate { get; set; }
    }
}
