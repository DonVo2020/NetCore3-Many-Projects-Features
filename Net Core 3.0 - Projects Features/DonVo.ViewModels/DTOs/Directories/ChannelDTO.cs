using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class ChannelDTO
    {
        public int ChannelKey { get; set; }
        public string ChannelLabel { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateChannelDTO
    {
        public string ChannelLabel { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDescription { get; set; }
    }
}
