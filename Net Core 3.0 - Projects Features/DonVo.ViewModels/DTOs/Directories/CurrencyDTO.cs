using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class CurrencyDTO
    {
        public int CurrencyKey { get; set; }
        public string CurrencyLabel { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateCurrencyDTO
    {
        public int CurrencyKey { get; set; }
        public string CurrencyLabel { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyDescription { get; set; }
    }
}
