using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimCustomer
    {
        public DimCustomer()
        {
            FactOnlineSales = new HashSet<FactOnlineSales>();
        }

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

        public virtual DimGeography GeographyKeyNavigation { get; set; }
        public virtual ICollection<FactOnlineSales> FactOnlineSales { get; set; }
    }
}
