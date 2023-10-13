using System;
using System.Collections.Generic;
using System.Text;

namespace MiniDefinition.ExchangeRateEntries
{
    public class ExchangeRateEntryLookupDto
    {
        public Guid? Id {  get; set; } 
        public DateTime Date { get; set; }
        public int? CustomsCode { get; set; }
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set; }
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get; set; }
        public decimal? FreeBuyExchangeRate { get; set; }
        public decimal? FreeSellExchangeRate { get; set; }
        public Guid? ExchangeRateEntryId { get; set; }
        public Guid? CurrencyId { get; set; }
    }
}
