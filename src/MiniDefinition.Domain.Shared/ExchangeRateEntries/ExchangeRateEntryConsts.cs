

namespace MiniDefinition.ExchangeRateEntries
{
    public static class ExchangeRateEntryConsts
    {
        private const string DefaultSorting = "{0}Id asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ExchangeRateEntry." : string.Empty);
        }

    }
}