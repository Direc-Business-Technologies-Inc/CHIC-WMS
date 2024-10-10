using static Application.Models.ViewModels.FormsAndReportsViewModel.PalletLabelSalesOrderDetailsViewModel;

namespace Application.Services.Repositories;

public interface IFormsAndReportsService
{
	IrradiationLabelPrintingViewModel InitializeIrradiationLabelPrinting();
	void FilterIrradiationSchedule(IrradiationLabelPrintingViewModel model, string start, string end);
	IrradiationSalesOrderDetailsViewModel InitilizeSalesOrderDetails(string SONo);
	PalletLabelViewModel InitializePalletLabelPrinting();
	void FilterPalletLabel(PalletLabelViewModel model, string start, string end);
	PalletLabelSalesOrderDetailsViewModel InitilizePalletLabelSalesOrderDetails(string SONo);
	bool SavePalletLabel(List<PalletLabelDetails> selectedPallets, PalletLabelSalesOrder palletLabelSalesOrder);
	FormsAndReportsViewModel InitializeFormsAndReports();
}