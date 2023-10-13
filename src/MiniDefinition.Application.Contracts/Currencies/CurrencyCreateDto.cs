using MiniDefinition.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace MiniDefinition.Currencies
{

    public class CurrencyCreateDto
    {
        
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public YesOrNoEnum? IsPassive { get; set; }
        public DateTime DatePassive { get; set; }
        public ApprovalStatusEnum? ApprovalStatus { get; set; }
        public Guid? ProcessID { get; set; }
        
        // jhipster-needle-dto-add-field - JHipster will add fields here, do not remove

       


        
        

    }
}


