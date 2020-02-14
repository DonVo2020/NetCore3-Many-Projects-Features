using DonVo.Persistences.Models;
using Microsoft.EntityFrameworkCore;

namespace DonVo.Persistences
{
    public partial class ContosoRetailDWContext : DbContext
    {
        public ContosoRetailDWContext()
        {
        }

        public ContosoRetailDWContext(DbContextOptions<ContosoRetailDWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DimAccount> DimAccount { get; set; }
        public virtual DbSet<DimChannel> DimChannel { get; set; }
        public virtual DbSet<DimCurrency> DimCurrency { get; set; }
        public virtual DbSet<DimCustomer> DimCustomer { get; set; }
        public virtual DbSet<DimDate> DimDate { get; set; }
        public virtual DbSet<DimEmployee> DimEmployee { get; set; }
        public virtual DbSet<DimEntity> DimEntity { get; set; }
        public virtual DbSet<DimGeography> DimGeography { get; set; }
        public virtual DbSet<DimMachine> DimMachine { get; set; }
        public virtual DbSet<DimOutage> DimOutage { get; set; }
        public virtual DbSet<DimProduct> DimProduct { get; set; }
        public virtual DbSet<DimProductCategory> DimProductCategory { get; set; }
        public virtual DbSet<DimProductSubcategory> DimProductSubcategory { get; set; }
        public virtual DbSet<DimPromotion> DimPromotion { get; set; }
        public virtual DbSet<DimSalesTerritory> DimSalesTerritory { get; set; }
        public virtual DbSet<DimScenario> DimScenario { get; set; }
        public virtual DbSet<DimStore> DimStore { get; set; }
        public virtual DbSet<FactExchangeRate> FactExchangeRate { get; set; }
        public virtual DbSet<FactInventory> FactInventory { get; set; }
        public virtual DbSet<FactItmachine> FactItmachine { get; set; }
        public virtual DbSet<FactItsla> FactItsla { get; set; }
        public virtual DbSet<FactOnlineSales> FactOnlineSales { get; set; }
        public virtual DbSet<FactSales> FactSales { get; set; }
        public virtual DbSet<FactSalesQuota> FactSalesQuota { get; set; }
        public virtual DbSet<FactStrategyPlan> FactStrategyPlan { get; set; }
        public virtual DbSet<VCustomer> VCustomer { get; set; }
        public virtual DbSet<VCustomerOrders> VCustomerOrders { get; set; }
        public virtual DbSet<VCustomerPromotion> VCustomerPromotion { get; set; }
        public virtual DbSet<VOnlineSalesOrder> VOnlineSalesOrder { get; set; }
        public virtual DbSet<VOnlineSalesOrderDetail> VOnlineSalesOrderDetail { get; set; }
        public virtual DbSet<VProductForecast> VProductForecast { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-ILQS92OM\\SQLEXPRESS;Database=ContosoRetailDW;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DimAccount>(entity =>
            {
                entity.HasKey(e => e.AccountKey)
                    .HasName("PK_DimAccount_AccountKey");

                entity.Property(e => e.AccountDescription).HasMaxLength(50);

                entity.Property(e => e.AccountLabel).HasMaxLength(100);

                entity.Property(e => e.AccountName).HasMaxLength(50);

                entity.Property(e => e.AccountType).HasMaxLength(50);

                entity.Property(e => e.CustomMemberOptions).HasMaxLength(200);

                entity.Property(e => e.CustomMembers).HasMaxLength(300);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.Operator).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ValueType).HasMaxLength(50);
            });

            modelBuilder.Entity<DimChannel>(entity =>
            {
                entity.HasKey(e => e.ChannelKey)
                    .HasName("PK_DimChannel_ChannelKey");

                entity.Property(e => e.ChannelDescription).HasMaxLength(50);

                entity.Property(e => e.ChannelLabel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ChannelName).HasMaxLength(20);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyKey)
                    .HasName("PK_DimCurrency_CurrencyKey");

                entity.HasIndex(e => e.CurrencyLabel)
                    .HasName("AK_DimCurrency_CurrencyLabel")
                    .IsUnique();

                entity.Property(e => e.CurrencyDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CurrencyLabel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerKey)
                    .HasName("PK_CustomerKey");

                entity.HasIndex(e => e.CustomerLabel)
                    .IsUnique();

                entity.Property(e => e.AddressLine1).HasMaxLength(120);

                entity.Property(e => e.AddressLine2).HasMaxLength(120);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CustomerLabel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerType).HasMaxLength(15);

                entity.Property(e => e.DateFirstPurchase).HasColumnType("date");

                entity.Property(e => e.Education).HasMaxLength(40);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.HouseOwnerFlag)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Occupation).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(8);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.YearlyIncome).HasColumnType("money");

                entity.HasOne(d => d.GeographyKeyNavigation)
                    .WithMany(p => p.DimCustomer)
                    .HasForeignKey(d => d.GeographyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DimCustomer_DimGeography");
            });

            modelBuilder.Entity<DimDate>(entity =>
            {
                entity.HasKey(e => e.Datekey)
                    .HasName("PK_DimDate_DateKey");

                entity.Property(e => e.Datekey).HasColumnType("datetime");

                entity.Property(e => e.AsiaSeason).HasMaxLength(50);

                entity.Property(e => e.CalendarDayOfWeekLabel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CalendarHalfYearLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CalendarMonthLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CalendarQuarterLabel).HasMaxLength(20);

                entity.Property(e => e.CalendarWeekLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CalendarYearLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DateDescription)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EuropeSeason).HasMaxLength(50);

                entity.Property(e => e.FiscalHalfYearLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FiscalMonthLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FiscalQuarterLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FiscalYearLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FullDateLabel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.HolidayName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IsWorkDay)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NorthAmericaSeason).HasMaxLength(50);
            });

            modelBuilder.Entity<DimEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeKey)
                    .HasName("PK_DimEmployee_EmployeeKey");

                entity.Property(e => e.BaseRate).HasColumnType("money");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EmergencyContactName).HasMaxLength(50);

                entity.Property(e => e.EmergencyContactPhone).HasMaxLength(25);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ParentEmployeeKeyNavigation)
                    .WithMany(p => p.InverseParentEmployeeKeyNavigation)
                    .HasForeignKey(d => d.ParentEmployeeKey)
                    .HasConstraintName("FK_DimEmployee_DimEmployee");
            });

            modelBuilder.Entity<DimEntity>(entity =>
            {
                entity.HasKey(e => e.EntityKey)
                    .HasName("PK_DimEntity_EntityKey");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EntityDescription).HasMaxLength(100);

                entity.Property(e => e.EntityLabel).HasMaxLength(100);

                entity.Property(e => e.EntityName).HasMaxLength(50);

                entity.Property(e => e.EntityType).HasMaxLength(100);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.ParentEntityLabel).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Current')");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimGeography>(entity =>
            {
                entity.HasKey(e => e.GeographyKey)
                    .HasName("PK_DimGeography_GeographyKey");

                entity.Property(e => e.CityName).HasMaxLength(100);

                entity.Property(e => e.ContinentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.GeographyType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.RegionCountryName).HasMaxLength(100);

                entity.Property(e => e.StateProvinceName).HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimMachine>(entity =>
            {
                entity.HasKey(e => e.MachineKey)
                    .HasName("PK_DimMachine_MachineKey");

                entity.Property(e => e.MachineKey).ValueGeneratedNever();

                entity.Property(e => e.DecommissionDate).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.MachineDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MachineHardware).HasMaxLength(100);

                entity.Property(e => e.MachineLabel).HasMaxLength(100);

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MachineOs)
                    .IsRequired()
                    .HasColumnName("MachineOS")
                    .HasMaxLength(50);

                entity.Property(e => e.MachineSoftware)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MachineSource)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MachineType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ServiceStartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.DimMachine)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DimMachine_DimStore");
            });

            modelBuilder.Entity<DimOutage>(entity =>
            {
                entity.HasKey(e => e.OutageKey)
                    .HasName("PK_DimOutage_OutageKey");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.OutageDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.OutageLabel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OutageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OutageSubType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OutageSubTypeDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.OutageType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OutageTypeDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimProduct>(entity =>
            {
                entity.HasKey(e => e.ProductKey)
                    .HasName("PK_DimProduct_ProductKey");

                entity.Property(e => e.AvailableForSaleDate).HasColumnType("datetime");

                entity.Property(e => e.BrandName).HasMaxLength(50);

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasMaxLength(10);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.ColorId)
                    .HasColumnName("ColorID")
                    .HasMaxLength(10);

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("ImageURL")
                    .HasMaxLength(150);

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.Manufacturer).HasMaxLength(50);

                entity.Property(e => e.ProductDescription).HasMaxLength(400);

                entity.Property(e => e.ProductLabel).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(500);

                entity.Property(e => e.ProductUrl)
                    .HasColumnName("ProductURL")
                    .HasMaxLength(150);

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.Property(e => e.SizeRange).HasMaxLength(50);

                entity.Property(e => e.SizeUnitMeasureId)
                    .HasColumnName("SizeUnitMeasureID")
                    .HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(7);

                entity.Property(e => e.StockTypeId)
                    .HasColumnName("StockTypeID")
                    .HasMaxLength(10);

                entity.Property(e => e.StockTypeName).HasMaxLength(40);

                entity.Property(e => e.StopSaleDate).HasColumnType("datetime");

                entity.Property(e => e.StyleId)
                    .HasColumnName("StyleID")
                    .HasMaxLength(10);

                entity.Property(e => e.StyleName).HasMaxLength(20);

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.Property(e => e.UnitOfMeasureId)
                    .HasColumnName("UnitOfMeasureID")
                    .HasMaxLength(10);

                entity.Property(e => e.UnitOfMeasureName).HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.WeightUnitMeasureId)
                    .HasColumnName("WeightUnitMeasureID")
                    .HasMaxLength(20);

                entity.HasOne(d => d.ProductSubcategoryKeyNavigation)
                    .WithMany(p => p.DimProduct)
                    .HasForeignKey(d => d.ProductSubcategoryKey)
                    .HasConstraintName("FK_DimProduct_DimProductSubcategory");
            });

            modelBuilder.Entity<DimProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryKey)
                    .HasName("PK_DimProductCategory_ProductCategoryKey");

                entity.HasIndex(e => e.ProductCategoryLabel)
                    .HasName("AK_DimProductCategory_ProductCategoryLabel")
                    .IsUnique();

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.ProductCategoryDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductCategoryLabel).HasMaxLength(100);

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimProductSubcategory>(entity =>
            {
                entity.HasKey(e => e.ProductSubcategoryKey)
                    .HasName("PK_DimProductSubcategory_ProductSubcategoryKey");

                entity.HasIndex(e => e.ProductSubcategoryLabel)
                    .HasName("AK_DimProductSubcategory_ProductSubcategoryLabel")
                    .IsUnique();

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.ProductSubcategoryDescription).HasMaxLength(100);

                entity.Property(e => e.ProductSubcategoryLabel).HasMaxLength(100);

                entity.Property(e => e.ProductSubcategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ProductCategoryKeyNavigation)
                    .WithMany(p => p.DimProductSubcategory)
                    .HasForeignKey(d => d.ProductCategoryKey)
                    .HasConstraintName("FK_DimProductSubcategory_DimProductCategory");
            });

            modelBuilder.Entity<DimPromotion>(entity =>
            {
                entity.HasKey(e => e.PromotionKey)
                    .HasName("PK_DimPromotion_PromotionKey");

                entity.HasIndex(e => e.PromotionLabel)
                    .HasName("AK_DimPromotion_PromotionLabel")
                    .IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionCategory).HasMaxLength(50);

                entity.Property(e => e.PromotionDescription).HasMaxLength(255);

                entity.Property(e => e.PromotionLabel).HasMaxLength(100);

                entity.Property(e => e.PromotionName).HasMaxLength(100);

                entity.Property(e => e.PromotionType).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimSalesTerritory>(entity =>
            {
                entity.HasKey(e => e.SalesTerritoryKey)
                    .HasName("PK_DimSalesTerritory_SalesTerritoryKey");

                entity.HasIndex(e => e.SalesTerritoryLabel)
                    .HasName("AK_DimSalesTerritory_SalesTerritoryLabel")
                    .IsUnique();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.SalesTerritoryCountry)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SalesTerritoryGroup).HasMaxLength(50);

                entity.Property(e => e.SalesTerritoryLabel).HasMaxLength(100);

                entity.Property(e => e.SalesTerritoryLevel).HasMaxLength(10);

                entity.Property(e => e.SalesTerritoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SalesTerritoryRegion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.GeographyKeyNavigation)
                    .WithMany(p => p.DimSalesTerritory)
                    .HasForeignKey(d => d.GeographyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DimSalesTerritory_DimGeography");
            });

            modelBuilder.Entity<DimScenario>(entity =>
            {
                entity.HasKey(e => e.ScenarioKey);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.ScenarioDescription).HasMaxLength(50);

                entity.Property(e => e.ScenarioLabel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ScenarioName).HasMaxLength(20);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DimStore>(entity =>
            {
                entity.HasKey(e => e.StoreKey)
                    .HasName("PK_DimStore_StoreKey");

                entity.Property(e => e.AddressLine1).HasMaxLength(100);

                entity.Property(e => e.AddressLine2).HasMaxLength(100);

                entity.Property(e => e.CloseDate).HasColumnType("datetime");

                entity.Property(e => e.CloseReason).HasMaxLength(20);

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LastRemodelDate).HasColumnType("datetime");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StoreDescription)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.StoreFax).HasMaxLength(14);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StorePhone).HasMaxLength(15);

                entity.Property(e => e.StoreType).HasMaxLength(15);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.Property(e => e.ZipCodeExtension).HasMaxLength(10);

                entity.HasOne(d => d.GeographyKeyNavigation)
                    .WithMany(p => p.DimStore)
                    .HasForeignKey(d => d.GeographyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DimStore_DimGeography");
            });

            modelBuilder.Entity<FactExchangeRate>(entity =>
            {
                entity.HasKey(e => e.ExchangeRateKey)
                    .HasName("PK_FactExchangeRate_ExchangeRateKey");

                entity.Property(e => e.DateKey).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactExchangeRate)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactExchangeRate_DimCurrency");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactExchangeRate)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactExchangeRate_DimDate");
            });

            modelBuilder.Entity<FactInventory>(entity =>
            {
                entity.HasKey(e => e.InventoryKey)
                    .HasName("PK_FactInventory_InventoryKey");

                entity.Property(e => e.DateKey).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactInventory)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInventory_DimCurrency");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactInventory)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInventory_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactInventory)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInventory_DimProduct");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.FactInventory)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactInventory_DimStore");
            });

            modelBuilder.Entity<FactItmachine>(entity =>
            {
                entity.HasKey(e => e.Itmachinekey);

                entity.ToTable("FactITMachine");

                entity.Property(e => e.Itmachinekey).HasColumnName("ITMachinekey");

                entity.Property(e => e.CostAmount).HasColumnType("money");

                entity.Property(e => e.CostType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Datekey).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.DatekeyNavigation)
                    .WithMany(p => p.FactItmachine)
                    .HasForeignKey(d => d.Datekey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactITMachine_DimDate");

                entity.HasOne(d => d.MachineKeyNavigation)
                    .WithMany(p => p.FactItmachine)
                    .HasForeignKey(d => d.MachineKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactITMachine_DimMachine");
            });

            modelBuilder.Entity<FactItsla>(entity =>
            {
                entity.HasKey(e => e.Itslakey)
                    .HasName("PK_FactITSLA_ITSLAKey");

                entity.ToTable("FactITSLA");

                entity.Property(e => e.Itslakey).HasColumnName("ITSLAkey");

                entity.Property(e => e.DateKey).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.OutageEndTime).HasColumnType("datetime");

                entity.Property(e => e.OutageStartTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactItsla)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactITSLA_DimDate");

                entity.HasOne(d => d.MachineKeyNavigation)
                    .WithMany(p => p.FactItsla)
                    .HasForeignKey(d => d.MachineKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactITSLA_DimMachine");

                entity.HasOne(d => d.OutageKeyNavigation)
                    .WithMany(p => p.FactItsla)
                    .HasForeignKey(d => d.OutageKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactITSLA_DimOutage");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.FactItsla)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactITSLA_DimStore");
            });

            modelBuilder.Entity<FactOnlineSales>(entity =>
            {
                entity.HasKey(e => e.OnlineSalesKey)
                    .HasName("PK_FactOnlineSales_SalesKey");

                entity.Property(e => e.DateKey).HasColumnType("datetime");

                entity.Property(e => e.DiscountAmount).HasColumnType("money");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnAmount).HasColumnType("money");

                entity.Property(e => e.SalesAmount).HasColumnType("money");

                entity.Property(e => e.SalesOrderNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactOnlineSales)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactOnlineSales_DimCurrency");

                entity.HasOne(d => d.CustomerKeyNavigation)
                    .WithMany(p => p.FactOnlineSales)
                    .HasForeignKey(d => d.CustomerKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactOnlineSales_DimCustomer");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactOnlineSales)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactOnlineSales_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactOnlineSales)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactOnlineSales_DimProduct");

                entity.HasOne(d => d.PromotionKeyNavigation)
                    .WithMany(p => p.FactOnlineSales)
                    .HasForeignKey(d => d.PromotionKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactOnlineSales_DimPromotion");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.FactOnlineSales)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactOnlineSales_DimStore");
            });

            modelBuilder.Entity<FactSales>(entity =>
            {
                entity.HasKey(e => e.SalesKey)
                    .HasName("PK_FactSales_SalesKey");

                entity.Property(e => e.ChannelKey).HasColumnName("channelKey");

                entity.Property(e => e.DateKey).HasColumnType("datetime");

                entity.Property(e => e.DiscountAmount).HasColumnType("money");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnAmount).HasColumnType("money");

                entity.Property(e => e.SalesAmount).HasColumnType("money");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ChannelKeyNavigation)
                    .WithMany(p => p.FactSales)
                    .HasForeignKey(d => d.ChannelKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSales_DimChannel");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactSales)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSales_DimCurrency");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactSales)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSales_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactSales)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSales_DimProduct");

                entity.HasOne(d => d.PromotionKeyNavigation)
                    .WithMany(p => p.FactSales)
                    .HasForeignKey(d => d.PromotionKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSales_DimPromotion");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.FactSales)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSales_DimStore");
            });

            modelBuilder.Entity<FactSalesQuota>(entity =>
            {
                entity.HasKey(e => e.SalesQuotaKey)
                    .HasName("PK_FactSalesQuota_SalesQuotaKey");

                entity.Property(e => e.DateKey).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.GrossMarginQuota).HasColumnType("money");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.SalesAmountQuota).HasColumnType("money");

                entity.Property(e => e.SalesQuantityQuota).HasColumnType("money");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.ChannelKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.ChannelKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimChannel");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimCurrency");

                entity.HasOne(d => d.DateKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.DateKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimDate");

                entity.HasOne(d => d.ProductKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.ProductKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimProduct");

                entity.HasOne(d => d.ScenarioKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.ScenarioKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimScenario");

                entity.HasOne(d => d.StoreKeyNavigation)
                    .WithMany(p => p.FactSalesQuota)
                    .HasForeignKey(d => d.StoreKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactSalesQuota_DimStore");
            });

            modelBuilder.Entity<FactStrategyPlan>(entity =>
            {
                entity.HasKey(e => e.StrategyPlanKey)
                    .HasName("PK_FactStrategyPlan_StrategyPlanKey");

                entity.Property(e => e.Amount).HasColumnType("float");

                entity.Property(e => e.Datekey).HasColumnType("datetime");

                entity.Property(e => e.EtlloadId).HasColumnName("ETLLoadID");

                entity.Property(e => e.LoadDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.AccountKeyNavigation)
                    .WithMany(p => p.FactStrategyPlan)
                    .HasForeignKey(d => d.AccountKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactStrategyPlan_DimAccount");

                entity.HasOne(d => d.CurrencyKeyNavigation)
                    .WithMany(p => p.FactStrategyPlan)
                    .HasForeignKey(d => d.CurrencyKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactStrategyPlan_DimCurrency");

                entity.HasOne(d => d.DatekeyNavigation)
                    .WithMany(p => p.FactStrategyPlan)
                    .HasForeignKey(d => d.Datekey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactStrategyPlan_DimDate");

                entity.HasOne(d => d.EntityKeyNavigation)
                    .WithMany(p => p.FactStrategyPlan)
                    .HasForeignKey(d => d.EntityKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactStrategyPlan_DimEntity");

                entity.HasOne(d => d.ProductCategoryKeyNavigation)
                    .WithMany(p => p.FactStrategyPlan)
                    .HasForeignKey(d => d.ProductCategoryKey)
                    .HasConstraintName("FK_FactStrategyPlan_DimProductCategory");

                entity.HasOne(d => d.ScenarioKeyNavigation)
                    .WithMany(p => p.FactStrategyPlan)
                    .HasForeignKey(d => d.ScenarioKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactStrategyPlan_DimScenario");
            });

            modelBuilder.Entity<VCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Customer");

                entity.Property(e => e.Consumption).HasColumnType("money");

                entity.Property(e => e.Education).HasMaxLength(40);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.HouseOwnerFlag)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.YearlyIncome).HasColumnType("money");
            });

            modelBuilder.Entity<VCustomerOrders>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_CustomerOrders");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.IncomeGroup)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Product).HasMaxLength(500);

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ProductSubcategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Region).HasMaxLength(100);
            });

            modelBuilder.Entity<VCustomerPromotion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_CustomerPromotion");

                entity.Property(e => e.Education).HasMaxLength(40);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.HouseOwnerFlag)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.PromotionName).HasMaxLength(100);

                entity.Property(e => e.PromotionType).HasMaxLength(50);

                entity.Property(e => e.YearlyIncome).HasColumnType("money");
            });

            modelBuilder.Entity<VOnlineSalesOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_OnlineSalesOrder");

                entity.Property(e => e.IncomeGroup)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Region).HasMaxLength(100);
            });

            modelBuilder.Entity<VOnlineSalesOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_OnlineSalesOrderDetail");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Product).HasMaxLength(500);
            });

            modelBuilder.Entity<VProductForecast>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_ProductForecast");

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.SalesAmount).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
