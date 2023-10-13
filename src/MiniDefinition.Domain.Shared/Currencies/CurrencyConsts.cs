

namespace MiniDefinition.Currencies
{
    public static class CurrencyConsts
    {
        private const string DefaultSorting = "{0}Id asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Currency." : string.Empty);
        }

    }
}