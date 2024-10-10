using static Application.Models.ViewModels.BinLabelViewModel;

namespace Application.Services.Repositories
{
	public interface IBinServices
	{
		BinMappingViewModel InitializeBinMapping(BinMappingViewModel model);
		void SelectInput(BinMappingViewModel model, string InputName, string value);
		ICollection<BinMappingPins> AdjustPinList(BinMappingViewModel model, string Method, BinMappingPins Data, float IntrinsicWidth, float RenderedWidth);
		void FetchColumn(BinMappingViewModel model);
		void FetchRows(BinMappingViewModel model, string Aisle);
		bool SaveBinMapping(BinMappingViewModel model);
		bool UploadImage(string ImgSrc, string FileName);
		bool GetBinMapping(BinMappingViewModel model);
		void GetShelf(BinMappingViewModel model, string WarehouseCode);
		List<WarehouseCode> GetWarehousesCodes();
		List<BinLabel> GetBinsData(string whsCode);
	}
}