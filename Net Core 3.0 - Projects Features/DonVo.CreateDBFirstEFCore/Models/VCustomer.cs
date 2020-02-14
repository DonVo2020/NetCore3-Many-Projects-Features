using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class VCustomer
    {
        public int CustomerKey { get; set; }
        public int? Age { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public decimal? YearlyIncome { get; set; }
        public byte? TotalChildren { get; set; }
        public byte? NumberChildrenAtHome { get; set; }
        public string Education { get; set; }
        public string HouseOwnerFlag { get; set; }
        public byte? NumberCarsOwned { get; set; }
        public decimal? Consumption { get; set; }
    }
}
