using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;


using MiniDefinition.Countries;
using MiniDefinition.Enums;
using MiniDefinition.Passive;

/// <summary>
///  Code Generator ile üretilen abstract sınıflarda özellestirme yapılabilmesi için abstract 
///  sinifi kalitim alınarak özelleştirme yapilmasi gerekmektedir.
///  Code Generator tekrar calistirildiğinda yapılan özellestirmeler kaybolacaktir!!! 

///  In order to be able to customize the abstract classes produced with Code Generator,
///  it is necessary to inherit the abstract class and customize it.
///  Restarting Code Generator, any customizations will be lost!!!
/// </summary>

namespace MiniDefinition.Cities
{
    
    public  class City : FullAuditedAggregateRoot<Guid>, IMultiTenant, IIsPassive
    {
        
        [StringLength(64,MinimumLength=0)]
        public string CityCode { get; set; }
        [StringLength(256,MinimumLength=0)]
        public string CityName { get; set; }
        public YesOrNoEnum? IsPassive { get; set; }
        public DateTime DatePassive { get; set; }
        public ApprovalStatusEnum? ApprovalStatus { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? CountryId { get; set; }    
        public Guid? TenantId { get; set; }
        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove


        public City()
        {

        }

        
        public City
        (
            Guid id
          ,string cityCode 
          ,string cityName 
          ,DateTime datePassive  
          ,YesOrNoEnum? isPassive
          ,ApprovalStatusEnum? approvalStatus
          ,Guid? countryId
            

        )


        {
               Id = id;
                CityCode=cityCode;
                CityName=cityName;
                DatePassive=datePassive;         
                 IsPassive=isPassive; 
                 ApprovalStatus=approvalStatus; 
                CountryId=countryId;

        }


        
    }
}
