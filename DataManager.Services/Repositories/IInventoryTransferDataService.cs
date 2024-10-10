using Application.Models.ViewModels;

namespace DataManager.Services.Repositories
{
    public interface IInventoryTransferDataService
    {
        List<BatchSerialViewModel> GetPalletBatches(string palletCode);
        List<InventoryTransfer> GetPallet(string palletCode, string bincode);
    }
}
