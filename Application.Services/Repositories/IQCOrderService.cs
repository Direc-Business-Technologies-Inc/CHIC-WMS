namespace Application.Services.Repositories
{
    public interface IQCOrderService
    {
        QCOrderViewModel InitializeQCOrder();
		List<QCOrderViewModel.ParameterDetail> GetParameters(string InspectionPlanCode, string Version);
		Task<(bool, string)> SaveQCOrder(QCOrderViewModel.QCOrderDetail model);
		Task<QCOrderViewModel.QCOrderDetail> SelectQCOrder(string QCOrderNo);
		Task<QCOrderViewModel.QCOrderDetail> SelectQCOrder(string ItemCode, string PlanType);
		Task<bool> PatchQCOrder(QCOrderViewModel.QCOrderDetail model);
		Task<CertificateOfIrradiation> PostCOI(string Method, QCOrderDetail model);
		CertificateOfIrradiation GetCertificateOfIrradiation(string QCOrderNo);
		Task<bool> UpdateSOStatus(int DocEntry);
	}
}