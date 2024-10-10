using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IBatchSerialService 
    {
        Task<List<BatchSerialViewModel.BatchSerial>> GetBatchSerialByMnfSerialLoc(string itemCode, string mnfSerial, string location);
        Task<List<BatchSerialViewModel.BatchSerial>> GetBatchSerialByMnfSerialLocUdf(string itemCode, string mnfSerial, string location, string udfName);
        Task<List<BinViewModel.BinMappingViewModel.BinAccumulator>> GetBinByBatchSerialLoc(string itemCode, string mnfSerial, string location);
        Task<List<BinViewModel.BinMappingViewModel.BinAccumulator>> GetBinByLoc(string location);
    }
}
