namespace Application.Services.Repositories
{
    public interface ICertificateOfIrradiationService
    {
        CertificateOfIrradiationViewModel InitializeCertificateOfIrradiation();

        Task<CertificateOfIrradiationViewModel.QCOrderDetail> SelectQCOrder(string QCOrderNo);

        Task<CertificateOfIrradiationViewModel> InitializeCertificateOfIrradiationDetails();

        List<CertificateOfIrradiationViewModel.ParameterDetail> GetParameters(string InspectionPlanCode, string Version);
        Task<CertificateOfIrradiationViewModel.COISalesOrderDetails> GetCOIDetails(string QCOrderNo);

        Task<bool> UpdateCOI(CertificateOfIrradiationViewModel.COISalesOrderDetails model);
        Task<bool> UpdateSOStatus(int DocEntry);
	}
}