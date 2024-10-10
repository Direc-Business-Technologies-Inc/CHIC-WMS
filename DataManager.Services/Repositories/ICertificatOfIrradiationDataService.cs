namespace DataManager.Services.Repositories
{
    public interface ICertificatOfIrradiationDataService
    {
        List<CertificateOfIrradiation> Get();
        CertificateOfIrradiation Get(string QCOrderNo);

        CertificateOfIrradiation Post(CertificateOfIrradiation model);

        CertificateOfIrradiation Patch(CertificateOfIrradiation model);
	}
}