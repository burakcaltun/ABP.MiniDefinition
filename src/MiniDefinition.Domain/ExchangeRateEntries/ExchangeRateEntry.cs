using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;


using MiniDefinition.Currencies;
using MiniDefinition.Passive;

/// <summary>
///  Code Generator ile üretilen abstract sınıflarda özellestirme yapılabilmesi için abstract 
///  sinifi kalitim alınarak özelleştirme yapilmasi gerekmektedir.
///  Code Generator tekrar calistirildiğinda yapılan özellestirmeler kaybolacaktir!!! 

///  In order to be able to customize the abstract classes produced with Code Generator,
///  it is necessary to inherit the abstract class and customize it.
///  Restarting Code Generator, any customizations will be lost!!!
/// </summary>

namespace MiniDefinition.ExchangeRateEntries
{
    
    public  class ExchangeRateEntry : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        
        public DateTime Date { get; set; }
        public int? CustomsCode { get; set; }
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set; }
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get; set; }
        public decimal? FreeBuyExchangeRate { get; set; }
        public decimal? FreeSellExchangeRate { get; set; }
        public Guid? ExchangeRateEntryId { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid? TenantId { get; set; }
        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove


        public ExchangeRateEntry()
        {

        }

        
        public ExchangeRateEntry
        (
            Guid id
          ,DateTime date  
          ,decimal? forexBuying 
          ,decimal? forexSelling 
          ,decimal? banknoteBuying 
          ,decimal? banknoteSelling 
          ,decimal? freeBuyExchangeRate 
          ,decimal? freeSellExchangeRate 
          ,int? customsCode

          ,Guid? currencyId
            

        )


        {
               Id = id;
                Date=date;         
                CustomsCode=customsCode;
                ForexBuying=forexBuying;        
                ForexSelling=forexSelling;        
                BanknoteBuying=banknoteBuying;        
                BanknoteSelling=banknoteSelling;        
                FreeBuyExchangeRate=freeBuyExchangeRate;        
                FreeSellExchangeRate=freeSellExchangeRate;        
                CurrencyId=currencyId;

        }


        
    }
}
