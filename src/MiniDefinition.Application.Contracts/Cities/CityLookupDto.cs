using MiniDefinition.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniDefinition.Cities
{
    public class CityLookupDto
    {
        public Guid? Id { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public YesOrNoEnum? IsPassive { get; set; }
        public DateTime DatePassive { get; set; }
        public ApprovalStatusEnum? ApprovalStatus { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? CountryId { get; set; }
    }
}
