
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using MiniDefinition.EntityFrameworkCore;
using MiniDefinition;
using MiniDefinition.Countries;
using MiniDefinition.Cities;
using MiniDefinition.ExchangeRateEntries;
using MiniDefinition.Currencies;


namespace MiniDefinition.EntityFrameworkCore
{
    [ConnectionStringName(MiniDefinitionDbProperties.ConnectionStringName)]
    public class MiniDefinitionDbContext :AbpDbContext<MiniDefinitionDbContext>,IMiniDefinitionDbContext
    {
       



        public MiniDefinitionDbContext(DbContextOptions<MiniDefinitionDbContext> options)
        : base(options)
    {

    }
       
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ExchangeRateEntry> ExchangeRateEntries { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



                builder.Entity<Country>(e=>{

                  e.Property(e => e.Code); 
                  e.Property(e => e.Name); 
                  e.Property(e => e.CustomsCode); 
                  e.Property(e => e.IsPassive); 
                  e.Property(e => e.DatePassive); 
                  e.Property(e => e.ApprovalStatus); 
                  e.Property(e => e.ProcessId); 
                      });
                builder.Entity<City>(e=>{

                  e.Property(e => e.CityCode); 
                  e.Property(e => e.CityName); 
                  e.Property(e => e.IsPassive); 
                  e.Property(e => e.DatePassive); 
                  e.Property(e => e.ApprovalStatus); 
                  e.Property(e => e.ProcessId); 
                e.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId).IsRequired();
                      });


                builder.Entity<ExchangeRateEntry>(e=>{

                  e.Property(e => e.Date); 
                  e.Property(e => e.CustomsCode); 
                  e.Property(e => e.ForexBuying); 
                  e.Property(e => e.ForexSelling); 
                  e.Property(e => e.BanknoteBuying); 
                  e.Property(e => e.BanknoteSelling); 
                  e.Property(e => e.FreeBuyExchangeRate); 
                  e.Property(e => e.FreeSellExchangeRate); 
                e.HasOne<Currency>().WithMany().HasForeignKey(x => x.CurrencyId).IsRequired();
                      });
                builder.Entity<Currency>(e=>{

                  e.Property(e => e.Code); 
                  e.Property(e => e.Name); 
                  e.Property(e => e.Number); 
                  e.Property(e => e.IsPassive); 
                  e.Property(e => e.DatePassive); 
                  e.Property(e => e.ApprovalStatus); 
                  e.Property(e => e.ProcessID); 
                      });
        
                    }

    

    }
}



            