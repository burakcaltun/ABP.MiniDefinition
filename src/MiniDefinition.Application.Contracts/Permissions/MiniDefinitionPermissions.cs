
namespace MiniDefinition.Permissions
{
public class MiniDefinitionPermissions
    {
        public const string GroupName = "MiniDefinition";

        public static class Countries
        {
            public const string Default =GroupName+".Countries";
            public const string Edit = Default+".Edit";
            public const string Create = Default+".Create";
            public const string Delete = Default+".Delete";
        }
        public static class Cities
        {
            public const string Default =GroupName+".Cities";
            public const string Edit = Default+".Edit";
            public const string Create = Default+".Create";
            public const string Delete = Default+".Delete";
        }

        public static class ExchangeRateEntries
        {
            public const string Default =GroupName+".ExchangeRateEntries";
            public const string Edit = Default+".Edit";
            public const string Create = Default+".Create";
            public const string Delete = Default+".Delete";
        }
        public static class Currencies
        {
            public const string Default =GroupName+".Currencies";
            public const string Edit = Default+".Edit";
            public const string Create = Default+".Create";
            public const string Delete = Default+".Delete";
        }

    }

}