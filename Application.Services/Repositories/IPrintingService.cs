namespace Application.Services.Repositories
{
    public interface IPrintingService
    {
		Task<HttpResponseMessage> Print(string Header, string args, string PrinterName, string FilePath, string Database);

	}
}