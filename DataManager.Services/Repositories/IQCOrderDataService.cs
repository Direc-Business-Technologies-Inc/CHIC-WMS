using DataManager.Models.QCOrder;

namespace DataManager.Services.Repositories
{
    public interface IQCOrderDataService
    {
        QCOrder PostQCOrder(QCOrder model);
        List<QCOrder> GetQCOrderList();
        QCOrder GetQCOrder(string QCOrderNo);
        QCOrder GetQCOrder(string ItemCode, string PlanType);
        QCOrder PatchQCOrder(QCOrder model);
	}
}