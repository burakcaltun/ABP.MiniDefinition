using Volo.Abp.Application.Dtos;
using System;
using MiniDefinition.Enums;

namespace MiniDefinition.Cities
{
    public class GetCitiesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string  CityCode { get; set; } 
        public string  CityName { get; set; } 
             
        public DateTime? DatePassive { get; set; } 
        public YesOrNoEnum? IsPassive { get; set; }  
        public ApprovalStatusEnum? ApprovalStatus { get; set; }  
        public GetCitiesInput()
        {

        }
    }
}
