using System.Collections.Generic;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class CustomerPromotionSampleData
    {
        internal static readonly List<CustomerPromotionData> customerPromotionDataList = new List<CustomerPromotionData>()
        {
            new CustomerPromotionData()
            {
                Age = 95,
                YearlyIncome = 90000.00F,
                TotalChildren = 0,
                NumberChildrenAtHome = 2,
                NumberCarsOwned = 2,
                ProductKey = 1155,
                PromotionKey = 8
            },
            new CustomerPromotionData()
            {
                Age = 44,
                YearlyIncome = 40000.00F,
                TotalChildren = 1,
                NumberChildrenAtHome = 1,
                NumberCarsOwned = 0,
                ProductKey = 1221,
                PromotionKey = 10
            },
            new CustomerPromotionData()
            {
                Age = 55,
                YearlyIncome = 180000.00F,
                TotalChildren = 3,
                NumberChildrenAtHome = 1,
                NumberCarsOwned = 0,
                ProductKey = 1155,
                PromotionKey = 10
            },
            new CustomerPromotionData()
            {
                Age = 57,
                YearlyIncome = 70000.00F,
                TotalChildren = 5,
                NumberChildrenAtHome = 5,
                NumberCarsOwned = 2,
                ProductKey = 1177,
                PromotionKey = 2
            },
            new CustomerPromotionData()
            {
                Age = 21,
                YearlyIncome = 10.00F,
                TotalChildren = 1,
                NumberChildrenAtHome = 3,
                NumberCarsOwned = 1,
                ProductKey = 1239,
                PromotionKey = 9
            },
        };
    }
}
