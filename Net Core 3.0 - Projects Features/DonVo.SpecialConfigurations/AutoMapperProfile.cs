using AutoMapper;
using DonVo.IdentityJwtBearer.Entities;
using DonVo.Persistences.Models;
using DonVo.ViewModels;
using DonVo.ViewModels.DTOs;
using DonVo.ViewModels.DTOs.Directories;
using Microsoft.AspNetCore.Identity;
using System;

namespace DonVo.SpecialConfigurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateAccountViewModel, CreateAccountDTO>();
            CreateMap<UpdatePersonalDataAccountViewModel, AccountDTO>().ReverseMap();
            CreateMap<UpdateEmailAccountViewModel, AccountDTO>().ReverseMap();
            CreateMap<UpdatePasswordAccountViewModel, UpdateAccountPasswordDTO>();
            CreateMap<UpdateRoleAccountViewModel, AccountDTO>().ReverseMap();

            CreateMap<User, AccountDTO>()
               .ForMember(d => d.HashId, c => c.MapFrom(x => x.Id))
               .ReverseMap();
            CreateMap<IdentityRole, RolesDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => x.Id))
                .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                .ReverseMap();
            CreateMap<RefreshTokenDTO, RefreshToken>()
                .ForMember(d => d.ClientId, c => c.MapFrom(x => x.ClientType));

            CreateMap<DimCustomer, CustomerDTO>()
                 .ForMember(d => d.CustomerKey, c => c.MapFrom(x => x.CustomerKey))
                 .ReverseMap();
            CreateMap<UpdateCustomerEmailAddressViewModel, CustomerDTO>().ReverseMap();
            CreateMap<UpdateCustomerCompanyNameViewModel, CustomerDTO>().ReverseMap();
            CreateMap<UpdateCustomerViewModel, CustomerDTO>().ReverseMap();

            CreateMap<DimAccount, AccountingDTO>()
                .ForMember(d => d.AccountKey, c => c.MapFrom(x => x.AccountKey))
                .ReverseMap();
            CreateMap<CreateMainAccountingViewModel, AccountingDTO>().ReverseMap();

            CreateMap<DimEmployee, EmployeeDTO>()
                .ForMember(d => d.EmployeeKey, c => c.MapFrom(x => x.EmployeeKey))
                .ReverseMap();
            CreateMap<CreateMainEmployeeViewModel, EmployeeDTO>().ReverseMap();

            CreateMap<DimPromotion, PromotionDTO>()
               .ForMember(d => d.PromotionKey, c => c.MapFrom(x => x.PromotionKey))
               .ReverseMap();
            CreateMap<CreateMainPromotionViewModel, PromotionDTO>().ReverseMap();

            CreateMap<DimCurrency, CurrencyDTO>()
               .ForMember(d => d.CurrencyKey, c => c.MapFrom(x => x.CurrencyKey))
               .ReverseMap();
            CreateMap<DimCurrency, CreateCurrencyDTO>()
               .ForMember(d => d.CurrencyKey, c => c.MapFrom(x => x.CurrencyKey))
               .ReverseMap();
            CreateMap<CreateMainCurrencyViewModel, CreateCurrencyDTO>().ReverseMap();

            CreateMap<DimStore, StoreDTO>()
               .ForMember(d => d.StoreKey, c => c.MapFrom(x => x.StoreKey))
               .ReverseMap();
            CreateMap<CreateMainStoreViewModel, StoreDTO>().ReverseMap();

            CreateMap<DimMachine, MachineDTO>()
              .ForMember(d => d.MachineKey, c => c.MapFrom(x => x.MachineKey))
              .ReverseMap();

            CreateMap<DimOutage, OutageDTO>()
              .ForMember(d => d.OutageKey, c => c.MapFrom(x => x.OutageKey))
              .ReverseMap();

            CreateMap<DimChannel, ChannelDTO>()
              .ForMember(d => d.ChannelKey, c => c.MapFrom(x => x.ChannelKey))
              .ReverseMap();

            CreateMap<DimEntity, EntityDTO>()
              .ForMember(d => d.EntityKey, c => c.MapFrom(x => x.EntityKey))
              .ReverseMap();

            CreateMap<DimScenario, ScenarioDTO>()
              .ForMember(d => d.ScenarioKey, c => c.MapFrom(x => x.ScenarioKey))
              .ReverseMap();

            CreateMap<DimProduct, ProductDTO>()
                .ForMember(d => d.ProductKey, c => c.MapFrom(x => x.ProductKey))
                .ReverseMap();

            CreateMap<DimProductCategory, ProductCategoryDTO>()
                .ForMember(d => d.ProductCategoryKey, c => c.MapFrom(x => x.ProductCategoryKey))
                .ReverseMap();

            CreateMap<DimProductSubcategory, ProductSubcategoryDTO>()
               .ForMember(d => d.ProductSubcategoryKey, c => c.MapFrom(x => x.ProductSubcategoryKey))
               .ReverseMap();
            CreateMap<DimProductSubcategory, CreateProductSubcategoryDTO>()
              .ForMember(d => d.ProductSubcategoryKey, c => c.MapFrom(x => x.ProductSubcategoryKey))
              .ReverseMap();
            CreateMap<DimProductSubcategory, UpdateProductSubcategoryDTO>()
              .ForMember(d => d.ProductSubcategoryKey, c => c.MapFrom(x => x.ProductSubcategoryKey))
              .ReverseMap();
            CreateMap<CreateMainProductSubcategoryViewModel, CreateProductSubcategoryDTO>().ReverseMap();
            CreateMap<UpdateProductSubcategoryViewModel, ProductSubcategoryDTO>().ReverseMap();
            CreateMap<UpdateProductSubcategoryViewModel, UpdateProductSubcategoryDTO>().ReverseMap();

            CreateMap<FactInventory, ResultSearchInventoryDTO>()
              .ForMember(d => d.InventoryKey, c => c.MapFrom(x => x.InventoryKey))
              .ReverseMap();

            CreateMap<FactItsla, ResultSearchItslaDTO>()
              .ForMember(d => d.Itslakey, c => c.MapFrom(x => x.Itslakey))
              .ReverseMap();

            CreateMap<FactSales, ResultSearchSalesDTO>()
             .ForMember(d => d.SalesKey, c => c.MapFrom(x => x.SalesKey))
             .ReverseMap();

            CreateMap<FactStrategyPlan, ResultSearchStrategyPlansDTO>()
            .ForMember(d => d.StrategyPlanKey, c => c.MapFrom(x => x.StrategyPlanKey))
            .ReverseMap();

        }
    }

    internal sealed class AutoMapperHelper
    {
        public static string GetHashId(string hash)
        {
            return hash.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public static uint GetRowVersion(string hash)
        {
            uint.TryParse(hash.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1], out var result);
            return result;
        }
    }
}