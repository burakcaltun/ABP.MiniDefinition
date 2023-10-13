using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using MiniDefinition.Currencies;

namespace MiniDefinition.ExchangeRateEntries
{
    
    public class ExchangeRateEntryWithNavigationPropertiesDto 
    {
    
        public ExchangeRateEntryDto  ExchangeRateEntry  {get; set;}
        
        public CurrencyDto Currency { get; set; }
        
       


        

        
       


        
    }
}
