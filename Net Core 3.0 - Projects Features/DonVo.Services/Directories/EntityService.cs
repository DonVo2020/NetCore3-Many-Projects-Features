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
    public class EntityService : IEntityService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public EntityService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<EntityDTO>>> GetAllAsync()
        {
            try
            {
                var entities = await _context.DimEntity.Where(z => !string.IsNullOrWhiteSpace(z.EntityName)).OrderBy(x => x.EntityName).ToListAsync();
                var result = _mapper.Map<IEnumerable<EntityDTO>>(entities);
                return new ActualResult<IEnumerable<EntityDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<EntityDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<EntityDTO>> GetAsync(int entityKey)
        {
            try
            {
                var entity = await _context.DimEntity.FindAsync(entityKey);
                if (entity == null)
                {
                    return new ActualResult<EntityDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<EntityDTO>(entity);
                return new ActualResult<EntityDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<EntityDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<EntityDTO>>> GetEntity(int entityKey)
        {
            try
            {
                var entity = await _context.DimEntity.Where(x => x.EntityKey == entityKey).OrderBy(x => x.EntityName).ToListAsync();
                var result = _mapper.Map<IEnumerable<EntityDTO>>(entity);
                return new ActualResult<IEnumerable<EntityDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<EntityDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainEntityAsync(CreateEntityDTO dto)
        {
            try
            {
                var entity = _mapper.Map<DimEntity>(dto);
                await _context.DimEntity.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = entity.EntityKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int entityKey)
        {
            try
            {
                var result = await _context.DimEntity.FindAsync(entityKey);
                if (result != null)
                {
                    _context.DimEntity.Remove(result);
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
