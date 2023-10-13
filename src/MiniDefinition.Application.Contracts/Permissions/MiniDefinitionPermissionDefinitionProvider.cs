using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using MiniDefinition.Localization;


namespace MiniDefinition.Permissions;

public class MiniDefinitionPermissionDefinitonProvider:PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {

        var myGroup = context.AddGroup(MiniDefinitionPermissions.GroupName,L("Permission:MiniDefinition "));

        
        var  countryPermission=myGroup.AddPermission(MiniDefinitionPermissions.Countries.Default,L("Permission:Countrys"));

        countryPermission.AddChild(MiniDefinitionPermissions.Countries.Create,L("Permission:Create"));
        countryPermission.AddChild(MiniDefinitionPermissions.Countries.Edit,L("Permission:Edit"));
        countryPermission.AddChild(MiniDefinitionPermissions.Countries.Delete,L("Permission:Delete"));
            
        
        
        var  cityPermission=myGroup.AddPermission(MiniDefinitionPermissions.Cities.Default,L("Permission:Citys"));

        cityPermission.AddChild(MiniDefinitionPermissions.Cities.Create,L("Permission:Create"));
        cityPermission.AddChild(MiniDefinitionPermissions.Cities.Edit,L("Permission:Edit"));
        cityPermission.AddChild(MiniDefinitionPermissions.Cities.Delete,L("Permission:Delete"));
            
        
     
            
        
        
        var  exchange_rate_entryPermission=myGroup.AddPermission(MiniDefinitionPermissions.ExchangeRateEntries.Default,L("Permission:ExchangeRateEntrys"));

        exchange_rate_entryPermission.AddChild(MiniDefinitionPermissions.ExchangeRateEntries.Create,L("Permission:Create"));
        exchange_rate_entryPermission.AddChild(MiniDefinitionPermissions.ExchangeRateEntries.Edit,L("Permission:Edit"));
        exchange_rate_entryPermission.AddChild(MiniDefinitionPermissions.ExchangeRateEntries.Delete,L("Permission:Delete"));
            
        
        
        var  currencyPermission=myGroup.AddPermission(MiniDefinitionPermissions.Currencies.Default,L("Permission:Currencys"));

        currencyPermission.AddChild(MiniDefinitionPermissions.Currencies.Create,L("Permission:Create"));
        currencyPermission.AddChild(MiniDefinitionPermissions.Currencies.Edit,L("Permission:Edit"));
        currencyPermission.AddChild(MiniDefinitionPermissions.Currencies.Delete,L("Permission:Delete"));
            
        
        }
            private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MiniDefinitionResource>(name);
        }


    }

