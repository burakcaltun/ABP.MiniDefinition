using Volo.Abp.Application.Dtos;
using System;
using MiniDefinition.Enums;

namespace MiniDefinition.Countries
{
    public class GetCountriesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string  Code { get; set; } 
        public string  Name { get; set; } 
             
        public DateTime? DatePassive { get; set; }  
        public int? CustomsCode { get; set; } 
        public YesOrNoEnum? IsPassive { get; set; }  
        public ApprovalStatusEnum? ApprovalStatus { get; set; }  
        public GetCountriesInput()
        {

        }
    }
}
