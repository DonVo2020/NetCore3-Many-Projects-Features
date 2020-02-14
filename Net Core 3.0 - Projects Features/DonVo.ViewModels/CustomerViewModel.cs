using DonVo.ViewModels.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    public class CreateMainCustomerViewModel
    {
        [Required(ErrorMessage = "FirstName cannot be blank!")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "LastName cannot be blank!")]
        public string LastName { get; set; }
        public string Education { get; set; }
        [Required(ErrorMessage = "MaritalStatus cannot be blank!")]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Gender cannot be blank!")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "EmailAddress cannot be blank!")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Phone cannot be blank!")]
        public string Phone { get; set; }
        public string AddressLine1 { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? YearlyIncome { get; set; }
        public byte? TotalChildren { get; set; }
        public string Title { get; set; }
        public string CustomerType { get; set; }
        public string Occupation { get; set; }
        [Required(ErrorMessage = "CompanyName cannot be blank!")]
        public string CompanyName { get; set; }
    }

    public class CreateCustomerCustomerViewModel
    {
        [Required]
        public int CustomerKey { get; set; }
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "FirstName cannot be blank!")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "LastName cannot be blank!")]
        public string LastName { get; set; }
        public string Education { get; set; }
        [Required(ErrorMessage = "MaritalStatus cannot be blank!")]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Gender cannot be blank!")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "EmailAddress cannot be blank!")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Phone cannot be blank!")]
        public string Phone { get; set; }
        public string Occupation { get; set; }
        [Required(ErrorMessage = "CompanyName cannot be blank!")]
        public string CompanyName { get; set; }
    }

    public class UpdateCustomerViewModel
    {
        public string HashIdMain { get; set; }
        [Required]
        public int CustomerKey { get; set; }
        //[Required(ErrorMessage = "FirstName cannot be blank!")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        //[Required(ErrorMessage = "LastName cannot be blank!")]
        public string LastName { get; set; }
        public string Education { get; set; }
        //[Required(ErrorMessage = "MaritalStatus cannot be blank!")]
        public string MaritalStatus { get; set; }
        //[Required(ErrorMessage = "Gender cannot be blank!")]
        public string Gender { get; set; }
        //[Required(ErrorMessage = "EmailAddress cannot be blank!")]
        public string EmailAddress { get; set; }
        //[Required(ErrorMessage = "Phone cannot be blank!")]
        public string Phone { get; set; }
        public string Occupation { get; set; }
        [Required(ErrorMessage = "CompanyName cannot be blank!")]
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public decimal YearlyIncome { get; set; }
    }

    public class UpdateCustomerFirstNameViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "FirstName cannot be empty!")]
        public string FirstName { get; set; }
    }

    public class UpdateCustomerMiddleNameViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "MiddleName cannot be empty!")]
        public string MiddleName { get; set; }
    }

    public class UpdateCustomerLastNameViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "LastName cannot be empty!")]
        public string LastName { get; set; }
    }

    public class UpdateCustomerEducationViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Education cannot be empty!")]
        public string Education { get; set; }
    }

    public class UpdateCustomerMaritalStatusViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "MaritalStatus cannot be empty!")]
        public string MaritalStatus { get; set; }
    }

    public class UpdateCustomerGenderViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Gender cannot be empty!")]
        public string Gender { get; set; }
    }

    public class UpdateCustomerEmailAddressViewModel
    {
        [Required]
        public int CustomerKey { get; set; }
        [Required(ErrorMessage = "EmailAddress cannot be empty!")]
        public string EmailAddress { get; set; }
    }

    public class UpdateCustomerPhoneViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Phone cannot be empty!")]
        public string Phone { get; set; }
    }

    public class UpdateCustomerOccupationViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required(ErrorMessage = "Occupation cannot be empty!")]
        public string Occupation { get; set; }
    }

    public class UpdateCustomerCompanyNameViewModel
    {     
        [Required]
        public int CustomerKey { get; set; }
        [Required(ErrorMessage = "CompanyName cannot be empty!")]
        public string CompanyName { get; set; }
    }


    [RestructuringCompare]
    public class RestructuringViewModel
    {
        [Required]
        public string HashIdMain { get; set; }
        [Required]
        public string HashIdCustomer { get; set; }
    }
}
