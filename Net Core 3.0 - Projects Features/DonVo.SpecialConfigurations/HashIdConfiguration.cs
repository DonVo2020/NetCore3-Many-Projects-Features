namespace DonVo.SpecialConfigurations
{
    public class RestConnection
    {
        public string DataAnalysisUrl { get; set; }
        public string ElasticUrl { get; set; }
    }

    public class HashIdConfiguration
    {
        public string Salt { get; set; }
        public int MinHashLenght { get; set; }
        public string Alphabet { get; set; }
        public bool UseGuidFormat { get; set; }
    }
}
