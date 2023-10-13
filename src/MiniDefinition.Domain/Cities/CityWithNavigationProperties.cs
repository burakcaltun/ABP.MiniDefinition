using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using MiniDefinition.Countries;

namespace MiniDefinition.Cities
{
    
    public class CityWithNavigationProperties 
    {
    
        public City  City  {get; set;}
        
        public Country Country { get; set; }
        
        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove


        

        
       


        
    }
}
