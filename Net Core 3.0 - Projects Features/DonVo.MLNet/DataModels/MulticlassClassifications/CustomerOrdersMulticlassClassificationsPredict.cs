using Microsoft.ML.Data;

namespace DonVo.MLNet.DataModels
{
    public class CustomerOrdersMulticlassClassificationsPredict
    {

        [ColumnName("PredictedLabel")]
        public string ProductCategoryName { get; set; }
    }
}
