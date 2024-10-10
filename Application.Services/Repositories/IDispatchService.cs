using Application.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.ViewModels.DispatchViewModel;

namespace Application.Services.Repositories
{
    public interface IDispatchService : IGenericService<DispatchModel>, IGenericAsyncService<DispatchModel>
    {
        List<SalesOrder> GetDispatchableSalesOrders();
        bool AllBoxIsForDispatch(string palletCode);
        bool AllBoxIsReleased(params string[] palletCodes);
        bool AllBoxIsDispatched(string palletCode);
        bool AllBoxIsDispatched(params string[] palletCodes);
    }
}
