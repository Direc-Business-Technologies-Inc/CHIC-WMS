using Application.Models;
using DataManager.Models.Enums;
using DataManager.Models.InventoryTransfer;
using System.Runtime.InteropServices;

namespace Application.Services.Repositories;

public interface IInventoryTransferService : IGenericService<InventoryTransferModel>, IGenericAsyncService<InventoryTransferModel>
{
    List<BatchSerialViewModel.BatchSerial> GetPalletBatches(string palletCode);
    (bool isValid, string message) ValidateScannedPallet(Pallet pallet, string binCode, TransferTypeEnum transferType);
    //Task<Application.Models.ViewModels.InventoryTransfer> GetAsync(string serviceType);
    Task<InventoryTransferViewModel.ServiceData> GetServiceData(string serviceType);
    Task<Dictionary<string, string>> GetTransferType();
    Pallet GetPalletMatrix(string itemCode, string palletCode, string binCode, string serviceType, [Optional] bool isManualTransfer);
    Task<Dictionary<string, string>> GetTransferTypeFromMatrix(string transferType, string serviceType);
    bool BinOccupied(string BinCode, string PalletCode);
    bool BinExists(string BinCode);
}
