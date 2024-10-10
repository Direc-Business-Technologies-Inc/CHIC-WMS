using Application.Models;

namespace Application.Services.Repositories
{
    public interface IPalletService : IGenericService<Pallet, string>, IGenericAsyncService<Pallet, string>
    {
        (int soNum, string palletSeries, int maxBoxNo) ExtractPalletDetails(string palletCode);
        bool IsForRelease(string palletCode);
        Task<string> GetIsExistBincode(string palletCode, string binCode);
    }
}
