using System.ComponentModel;

namespace DataManager.Models.Enums;

public static class Locations
{
    public static string Receiving = "Receiving";
    public static string Storage = "Storage";
    public static string LoadingBay = "Loading Bay";
    public static string UnloadingBay = "Unloading Bay";
    public static string Dispatch = "Dispatch";
    public static string NonConformity = "Non-Conformity Item";
    public static Dictionary<string, string> GetDict() => new Dictionary<string, string> { 
        { nameof(Receiving), Receiving },
        { nameof(Storage), Storage },
        { nameof(UnloadingBay), UnloadingBay },
        { nameof(Dispatch), Dispatch },
        { nameof(NonConformity), NonConformity }
    };
}

public enum TransferTypeEnum
{
    [Description("For Storage")] ForStorage,
    [Description("For Irradiation")] ForIrradiation,
    [Description("For Release")] ForRelease,
    [Description("Non-Conformity Item")] NonConformityItem,
    [Description("At Irradiation")] AtIrradiation

}

public static class TransferType
{
    public static Dictionary<TransferTypeEnum, string> Data { 
        get 
        {
            Dictionary<TransferTypeEnum, string> x = new();
            foreach (TransferTypeEnum val in Enum.GetValues(typeof(TransferTypeEnum)))
            {
                x.Add(val, val.GetDescription());
            }
            return x;
        }
    }
}