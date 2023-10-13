using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using MiniDefinition.ExchangeRateEntries;

namespace MiniDefinition.ExchangeRateEntries
{

    public class ExchangeRateEntryUpdateDto: IHasConcurrencyStamp
    {
        public Guid Id { get; set; }
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
        public string ConcurrencyStamp { get; set; }
        // jhipster-needle-dto-add-field - JHipster will add fields here, do not remove




        
        

    }
}


