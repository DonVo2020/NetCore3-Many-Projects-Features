using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IChannelService : IDisposable, IDirectory<ChannelDTO>
    {
        Task<ActualResult<ChannelDTO>> GetAsync(int channelKey);
        Task<ActualResult<IEnumerable<ChannelDTO>>> GetChannel(int channelKey);

        Task<ActualResult<string>> CreateMainChannelAsync(CreateChannelDTO dto);

        Task<ActualResult> DeleteAsync(int channelKey);
    }
}
