using Volo.Abp.Application.Dtos;
using System;

namespace MiniDefinition.ExchangeRateEntries
{
    public class GetExchangeRateEntriesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
             
        public DateTime? Date { get; set; } 
        public decimal? ForexBuying { get; set; } 
        public decimal? ForexSelling { get; set; } 
        public decimal? BanknoteBuying { get; set; } 
        public decimal? BanknoteSelling { get; set; } 
        public decimal? FreeBuyExchangeRate { get; set; } 
        public decimal? FreeSellExchangeRate { get; set; }  
        public int? CustomsCode { get; set; }
        public GetExchangeRateEntriesInput()
        {

        }
    }
}
