using Microsoft.ML.Data;

namespace DonVo.MLNet.DataModels
{
    public class CustomerPromotionBinaryClassificationsData
    {
        [LoadColumn(0)]
        public float Age { get; set; }
        [LoadColumn(1)]
        public float YearlyIncome { get; set; }
        [LoadColumn(2)]
        public float TotalChildren { get; set; }
        [LoadColumn(3)]
        public float NumberChildrenAtHome { get; set; }
        [LoadColumn(4)]
        public float NumberCarsOwned { get; set; }
        [LoadColumn(5)]
        public float ProductKey { get; set; }
        [LoadColumn(6)]
        public float PromotionKey { get; set; }
        [LoadColumn(7)]
        public bool HouseOwnerFlag { get; set; }
        //[LoadColumn(8)]
        //public bool Label { get; set; }

    }
}
