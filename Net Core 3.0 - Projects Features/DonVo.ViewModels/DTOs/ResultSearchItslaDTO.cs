using System;

namespace DonVo.ViewModels.DTOs
{
    public class ResultSearchItslaDTO
    {
        public int Itslakey { get; set; }
        public DateTime DateKey { get; set; }
        public string StoreName { get; set; }
        public string MachineName { get; set; }
        public string OutageName { get; set; }
        public DateTime OutageStartTime { get; set; }
        public DateTime OutageEndTime { get; set; }
        public int DownTime { get; set; }
    }
}
