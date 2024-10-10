using DataManager.Models.Receiving;
using Application.Libraries.SAP.SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.ViewModels.ReceivingViewModel;

namespace Application.Services.Repositories
{
    public interface IReceivingService : IGenericService<ReceivingModel>, IGenericAsyncService<ReceivingModel>
    {
        (string SoNum, string PalletSeries, double BoxNo) GetPalletDetails(string palletCode);
        public List<SalesOrder> GetSalesOrderList();
    }
}
