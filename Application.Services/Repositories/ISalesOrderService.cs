using Application.Libraries.SAP.SL;
using Application.Libraries.SAP.SL;
using Application.Models;
using DataManager.Models.Enums;

namespace Application.Services.Repositories
{
    public interface ISalesOrderService : IGenericService<Document>
    {
        List<Pallet> GetPallets(int docNum);
        string GetStatus(int docNum);
        public bool StatusIs(int docNum, string status);
        public int GetDocEntry(int docNum);
        public ServiceType GetServiceType(int docEntry);
        Task UpdateSalesOrderStatus(int docNum, string status);
    }
}
