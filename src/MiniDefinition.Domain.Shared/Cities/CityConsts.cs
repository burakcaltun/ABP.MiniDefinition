

namespace MiniDefinition.Cities
{
    public static class CityConsts
    {
        private const string DefaultSorting = "{0}Id asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "City." : string.Empty);
        }

    }
}