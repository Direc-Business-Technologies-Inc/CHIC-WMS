namespace DataManager.Services.Repositories;

    public interface IBinDataServices
    {
	BinMapping PostBinMapping(BinMapping model);

	List<BinMapping> GetBinMapping();

	BinMapping GetBinMapping(string WarehouseCode, string Shelf);

	BinMapping PatchBinMapping(string WarehouseCode, string Shelf, BinMapping model);

	List<BinAssignment> SavePalletLabel(List<BinAssignment> model);
	List<BinAssignment> GetPalletLabelPerSO(string SONo);

	List<BinAssignment> GetOccupiedPalletLabel();
	bool ClearPalletLabel(List<string> Pallets);
	bool BinOccupied(string BinCode, string PalletCode);
}