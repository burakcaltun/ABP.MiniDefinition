using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using MiniDefinition.Countries;

namespace MiniDefinition.Cities
{
    
    public class CityWithNavigationPropertiesDto 
    {
    
        public CityDto  City  {get; set;}
        
        public CountryDto Country { get; set; }
        
       


        

        
       


        
    }
}
