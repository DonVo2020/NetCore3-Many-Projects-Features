using AutoMapper;
using DonVo.Persistences;
using DonVo.Persistences.Models;
using DonVo.Services.ActualResults;
using DonVo.Services.Enums;
using DonVo.Services.Helpers;
using DonVo.Services.Interfaces.Directories;
using DonVo.ViewModels.DTOs.Directories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.Services.Directories
{
    public class ChannelService : IChannelService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public ChannelService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ChannelDTO>>> GetAllAsync()
        {
            try
            {
                var channels = await _context.DimChannel.Where(z => !string.IsNullOrWhiteSpace(z.ChannelName)).OrderBy(x => x.ChannelName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ChannelDTO>>(channels);
                return new ActualResult<IEnumerable<ChannelDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ChannelDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ChannelDTO>> GetAsync(int channelKey)
        {
            try
            {
                var channel = await _context.DimChannel.FindAsync(channelKey);
                if (channel == null)
                {
                    return new ActualResult<ChannelDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ChannelDTO>(channel);
                return new ActualResult<ChannelDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ChannelDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<ChannelDTO>>> GetChannel(int channelKey)
        {
            try
            {
                var channel = await _context.DimChannel.Where(x => x.ChannelKey == channelKey).OrderBy(x => x.ChannelName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ChannelDTO>>(channel);
                return new ActualResult<IEnumerable<ChannelDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ChannelDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainChannelAsync(CreateChannelDTO dto)
        {
            try
            {
                var channel = _mapper.Map<DimChannel>(dto);
                await _context.DimChannel.AddAsync(channel);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = channel.ChannelKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int channelKey)
        {
            try
            {
                var result = await _context.DimChannel.FindAsync(channelKey);
                if (result != null)
                {
                    _context.DimChannel.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
