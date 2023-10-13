using MiniDefinition.Enums;
using System;
using MiniDefinition.Shared;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MiniDefinition.Countries
{
    public class CountryLookupDto
    {

        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public ApprovalStatusEnum? ApprovalStatus { get; set; }
        public DateTime? DatePassive { get; set; }
        public int? CustomsCode { get; set; }
        public YesOrNoEnum? IsPassive { get; set; }
        public Guid? ProcessId { get; set; }
    }
}
