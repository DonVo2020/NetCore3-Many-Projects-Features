using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    public class CreateMainEmployeeViewModel
    {
        [Required(ErrorMessage = "EmployeeKey cannot be blank!")]
        public int EmployeeKey { get; set; }
        public int? ParentEmployeeKey { get; set; }
        [Required(ErrorMessage = "FirstName cannot be blank!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName cannot be blank!")]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "HireDate cannot be blank!")]
        public DateTime? HireDate { get; set; }
        [Required(ErrorMessage = "BirthDate cannot be blank!")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "EmailAddress cannot be blank!")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Phone cannot be blank!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "MaritalStatus cannot be blank!")]
        public string MaritalStatus { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        [Required(ErrorMessage = "Gender cannot be blank!")]
        public string Gender { get; set; }
        public decimal? BaseRate { get; set; }
        public short? VacationHours { get; set; }
        [Required(ErrorMessage = "DepartmentName cannot be blank!")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "StartDate cannot be blank!")]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Status cannot be blank!")]
        public string Status { get; set; }
    }
}
