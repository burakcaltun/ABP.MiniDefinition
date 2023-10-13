using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using MiniDefinition.Passive;
using MiniDefinition.Enums;
using MiniDefinition.Localization;
using Microsoft.Extensions.Localization;

namespace MiniDefinition.Countries.Passive
{
    public class IsPassive<T> where T : IIsPassive, IEntity
    {
        private readonly IStringLocalizer<MiniDefinitionResource> _localizer;
        public IsPassive(IStringLocalizer<MiniDefinitionResource> localizer)
        {
            _localizer = localizer;
        }
        public IQueryable<T> GetQueryablewithPassiveAsync(IQueryable<T> getStockType, string? isActive)

        {
            isActive = string.IsNullOrWhiteSpace(isActive) ? "A" : isActive;
            int IsActibeInt = isActive.ToUpper() == "P" ? 1 : 0;
            YesOrNoEnum yesOrNo = (YesOrNoEnum)IsActibeInt;
            if (!(new string[] { "A", "P", "T" }).Any(x => x == isActive.ToUpper()))
                throw new UserFriendlyException(_localizer["Def:Message:StockTypes:IsActiveDoluOlmali"]);
            if (isActive.ToUpper() != "T")
                getStockType = getStockType.Where(stktyp => stktyp.IsPassive == (YesOrNoEnum)IsActibeInt);
            return getStockType;
        }

    }
}
