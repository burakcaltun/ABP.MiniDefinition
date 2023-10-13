using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;


using MiniDefinition.Cities;
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

namespace MiniDefinition.Countries
{
    
    public  class Country : FullAuditedAggregateRoot<Guid>, IMultiTenant,  IIsPassive
    {
        
        [StringLength(64,MinimumLength=0)]
        public string Code { get; set; }
        [StringLength(256,MinimumLength=0)]
        public string Name { get; set; }
        public int? CustomsCode { get; set; }
        public YesOrNoEnum? IsPassive { get; set; }
        public DateTime DatePassive { get; set; }
        public ApprovalStatusEnum? ApprovalStatus { get; set; }
        public Guid? ProcessId { get; set; }
        
        public Guid? TenantId { get; set; }
        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove


        public Country()
        {

        }

        
        public Country
        (
            Guid id
          ,string code 
          ,string name 
          ,DateTime datePassive  
          ,int? customsCode

          ,YesOrNoEnum? isPassive
          ,ApprovalStatusEnum? approvalStatus
            

        )


        {
               Id = id;
                Code=code;
                Name=name;
                DatePassive=datePassive;         
                CustomsCode=customsCode;
                 IsPassive=isPassive; 
                 ApprovalStatus=approvalStatus; 

        }


        
    }
}
