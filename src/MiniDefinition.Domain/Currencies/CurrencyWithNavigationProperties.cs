using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using MiniDefinition.ExchangeRateEntries;

namespace MiniDefinition.Currencies
{
    
    public class CurrencyWithNavigationProperties 
    {
    
        public Currency  Currency  {get; set;}
        
        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove


        

        
       


        
    }
}
