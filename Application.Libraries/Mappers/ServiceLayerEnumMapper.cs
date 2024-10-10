using Application.Libraries.SAP.SL;
using AutoMapper;

namespace Application.Libraries.Mappers
{

    public class ServiceLayerEnumMapper : Profile
    {

        public ServiceLayerEnumMapper()
        {
            CreateMap<string, BoIssueMethod>()
                .ConvertUsing(s => MapBoIssueMethod(s));
            CreateMap<int?, ProductionItemType>()
                .ConvertUsing(s => MapProductionItemType(s));
            CreateMap<string, ResourceAllocationEnum>()
                .ConvertUsing(s => MapResourceAllocation(s));
            CreateMap<string, BoYesNoEnum>()
                .ConvertUsing(s => MapBoYesNoEnum(s));

            CreateMap<bool, BoYesNoEnum>()
                .ConvertUsing(s => MapBoYesNoEnum(s));

            CreateMap<string, BatchDetailServiceStatusEnum>()
                .ConvertUsing(s => MapBatchDetailServiceStatusEnum(s));

            //CreateMap<string, BoDocumentTypes>()
            //    .ConvertUsing(s => MapBoDocumentTypes(s));

            Func<string, BoDocumentTypes> haha = s =>
            {
                var r = MapBoDocumentTypes(s);
                if (r is null) throw new InvalidOperationException("No value matched");
                return r.Value;
            };

            CreateMap<string, BoDocumentTypes>()
            .ConvertUsing(s => haha(s));

            CreateMap<string, PrintStatusEnum>()
                .ConvertUsing(s => MapPrintStatusEnum(s));

            CreateMap<string, BoObjectTypes>()
                .ConvertUsing(s => MapBoObjectTypes(s));


            CreateMap<string, EDocStatusEnum>()
                .ConvertUsing(s => MapEDocStatusEnum(s));

            CreateMap<string, BoItemTreeTypes>()
                .ConvertUsing(s => MapBoItemTreeTypes(s));


            CreateMap<string, BoGLMethods>()
                .ConvertUsing(s => MapBoGLMethods(s));


            CreateMap<string, BoTaxTypes>()
                .ConvertUsing(s => MapBoTaxTypes(s));


            CreateMap<string, ItemTypeEnum>()
                .ConvertUsing(s => MapItemTypeEnum(s));


            CreateMap<string, BoPlanningSystem?>()
                .ConvertUsing(s => MapBoPlanningSystem(s));


            CreateMap<string, BoProcurementMethod?>()
                .ConvertUsing(s => MapBoProcurementMethod(s));

            CreateMap<string, BoMRPComponentWarehouse?>()
                .ConvertUsing(s => MapBoMRPComponentWarehouse(s));

            CreateMap<short?, IssuePrimarilyByEnum?>()
                .ConvertUsing(s => MapIssuePrimarilyByEnum(s));


            CreateMap<string, AssetStatusEnum?>()
                .ConvertUsing(s => MapAssetStatusEnum(s));

        }

        public AssetStatusEnum MapAssetStatusEnum(string value) => value switch
        {
            "N" => AssetStatusEnum.New,
            "A" => AssetStatusEnum.Active,
            "I" => AssetStatusEnum.Inactive,
            _ => default
        };

        public IssuePrimarilyByEnum MapIssuePrimarilyByEnum(short? value) => value switch
        {
            0 => IssuePrimarilyByEnum.IpbSerialAndBatchNumbers,
            1 => IssuePrimarilyByEnum.IpbBinLocations,
            _ => default
        };

        public BoMRPComponentWarehouse MapBoMRPComponentWarehouse(string value) => value switch
        {
            "B" => BoMRPComponentWarehouse.Bomcw_BOM,
            "P" => BoMRPComponentWarehouse.Bomcw_Parent,
            _ => default
        };

        public BoProcurementMethod MapBoProcurementMethod(string value) => value switch
        {
            "B" => BoProcurementMethod.Bom_Buy,
            "M" => BoProcurementMethod.Bom_Make,
            _ => default
        };

        public BoPlanningSystem MapBoPlanningSystem(string value) => value switch
        {
            "M" => BoPlanningSystem.Bop_MRP,
            "N" => BoPlanningSystem.Bop_None,
            _ => default
        };

        public ItemTypeEnum MapItemTypeEnum(string value) => value switch
        {
            "I" => ItemTypeEnum.ItItems,
            "L" => ItemTypeEnum.ItLabor,
            "T" => ItemTypeEnum.ItTravel,
            "F" => ItemTypeEnum.ItFixedAssets,
            _ => default
        };

        public BoTaxTypes MapBoTaxTypes(string value) => value switch
        {
            "Y" => BoTaxTypes.Tt_Yes,
            "U" => BoTaxTypes.Tt_UseTax,
            "N" => BoTaxTypes.Tt_No,
            _ => default
        };

        public BoGLMethods MapBoGLMethods(string value)
        => value switch
        {
            "W" => BoGLMethods.Glm_WH,
            "C" => BoGLMethods.Glm_ItemClass,
            "L" => BoGLMethods.Glm_ItemLevel,
            _ => default
        };

        public BoItemTreeTypes MapBoItemTreeTypes(string value) => value switch
        {
            "N" => BoItemTreeTypes.INotATree,
            "A" => BoItemTreeTypes.IAssemblyTree,
            "S" => BoItemTreeTypes.ISalesTree,
            "P" => BoItemTreeTypes.IProductionTree,
            "T" => BoItemTreeTypes.ITemplateTree,
            _ => default
        };

        private EDocStatusEnum MapEDocStatusEnum(string value) => value switch
        {
            "N" => EDocStatusEnum.Edoc_New,
            "P" => EDocStatusEnum.Edoc_Pending,
            "S" => EDocStatusEnum.Edoc_Sent,
            "E" => EDocStatusEnum.Edoc_Error,
            "C" => EDocStatusEnum.Edoc_Ok,
            _ => default
        };

        private BoObjectTypes MapBoObjectTypes(string value)
        {
            int val;
            if (int.TryParse(value, out val))
            {
                BoObjectTypes x = (BoObjectTypes)val;
                return x;
            }
            return default;
        }

        private PrintStatusEnum MapPrintStatusEnum(string value) => value switch
        {
            "Y" => PrintStatusEnum.PsYes,
            "N" => PrintStatusEnum.PsNo,
            "A" => PrintStatusEnum.PsAmended,
            _ => default
        };

        private BatchDetailServiceStatusEnum MapBatchDetailServiceStatusEnum(string value) => value switch
        {
            "1" => BatchDetailServiceStatusEnum.BdsStatus_Released,
            "2" => BatchDetailServiceStatusEnum.BdsStatus_NotAccessible,
            "3" => BatchDetailServiceStatusEnum.BdsStatus_Locked,
            _ => default
        };

        private BoDocumentTypes? MapBoDocumentTypes(string value) => value switch
        {
            "I" => BoDocumentTypes.DDocument_Items,
            "S" => BoDocumentTypes.DDocument_Service,
            _ => default
        };
        private BoYesNoEnum MapBoYesNoEnum(bool value) => value ? BoYesNoEnum.TYES : BoYesNoEnum.TNO;

        private BoYesNoEnum MapBoYesNoEnum(string value)
        => value switch
        {
            "Y" => BoYesNoEnum.TYES,
            "N" => BoYesNoEnum.TNO,
            _ => default
        };

        private BoIssueMethod MapBoIssueMethod(string value) => value switch
        {
            "M" => BoIssueMethod.Im_Manual,
            "B" => BoIssueMethod.Im_Backflush,
        };


        private ProductionItemType MapProductionItemType(int? value) => value switch
        {
            4 => ProductionItemType.Pit_Item,
            290 => ProductionItemType.Pit_Resource,
            -18 => ProductionItemType.Pit_Text,
            _ => default
        };
        private ResourceAllocationEnum MapResourceAllocation(string value) => value switch
        {
            "D" => ResourceAllocationEnum.RaEndDateBackwards,
            "B" => ResourceAllocationEnum.RaOnStartDate,
            _ => default
        };
    }
}
