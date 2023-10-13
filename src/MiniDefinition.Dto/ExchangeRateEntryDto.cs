using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniDefinition.Dto
{

    public class ExchangeRateEntryDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int? CustomsCode { get; set; }
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set; }
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get; set; }
        public decimal? FreeBuyExchangeRate { get; set; }
        public decimal? FreeSellExchangeRate { get; set; }
        public long? ExchangeRateEntryId { get; set; }
        public CurrencyDto ExchangeRateEntry { get; set; }

        // jhipster-needle-dto-add-field - JHipster will add fields here, do not remove
    }
}
