using System;
using System.Collections.Generic;

namespace DonVo.ViewModels.DTOs.Directories
{
    public abstract class BaseUpdateDTO
    {
        //public virtual string HashId { get; set; }
        public virtual string HashIdMain { get; set; }
    }

    public class CustomerDTO : BaseUpdateDTO
    {       
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public int GeographyKey { get; set; }
        public string CustomerLabel { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool? NameStyle { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public decimal? YearlyIncome { get; set; }
        public byte? TotalChildren { get; set; }
        public byte? NumberChildrenAtHome { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string HouseOwnerFlag { get; set; }
        public byte? NumberCarsOwned { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Phone { get; set; }
        public DateTime? DateFirstPurchase { get; set; }
        public string CustomerType { get; set; }
        public string CompanyName { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateCustomerDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Education { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
    }

    public class CreateCustomerCustomerDTO : CreateCustomerDTO
    {
        public int CustomerKey { get; set; }
    }

    public class UpdateCustomerDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Education { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
    }

    public class UpdateCustomerFirstNameDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string FirstName { get; set; }
    }
    public class UpdateCustomerMiddleNameTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string MiddleName { get; set; }
    }
    public class UpdateCustomerLastNameDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string LastName { get; set; }
    }
    public class UpdateCustomerEducationDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string Education { get; set; }
    }
    public class UpdateCustomerMaritalStatusDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string MaritalStatus { get; set; }
    }
    public class UpdateCustomerGenderDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string Gender { get; set; }
    }
    public class UpdateCustomerEmailAddressDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string EmailAddress { get; set; }
    }
    public class UpdateCustomerPhoneDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string Phone { get; set; }
    }
    public class UpdateCustomerOccupationDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string Occupation { get; set; }
    }
    public class UpdateCustomerCompanyNameDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string CompanyName { get; set; }
    }


    public class RestructuringCustomerDTO : BaseUpdateDTO
    {
        public override string HashIdMain { get; set; }
        public int CustomerKey { get; set; }
        public string HashIdCustomer { get; set; }
    }
    public class TreeCustomersDTO
    {
        public string GroupName { get; set; }
        public IEnumerable<CustomerDTO> Customers { get; set; }
    }
}
