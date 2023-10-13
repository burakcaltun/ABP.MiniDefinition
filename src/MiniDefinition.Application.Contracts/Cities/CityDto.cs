using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using MiniDefinition.Countries;
using MiniDefinition.Enums;

namespace MiniDefinition.Cities
{

    public class CityDto:FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public YesOrNoEnum? IsPassive { get; set; }
        public DateTime DatePassive { get; set; }
        public ApprovalStatusEnum? ApprovalStatus { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? CountryId { get; set; }
        
        public string ConcurrencyStamp { get; set; }      
        // jhipster-needle-dto-add-field - JHipster will add fields here, do not remove




        
        

    }
}


