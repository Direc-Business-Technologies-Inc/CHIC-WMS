using Application.Libraries.SAP;
using DataManager.Services.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Application.Models.ViewModels.BinLabelViewModel;
using static Application.Models.ViewModels.ConfigurationViewModel;
using static Application.Models.ViewModels.FormsAndReportsViewModel.PalletLabelSalesOrderDetailsViewModel;

namespace Application.Services.Core;

public class FormsAndReportsService : IFormsAndReportsService
{
	private readonly IMsSqlDataAccess _sql;
	private readonly IQCMaintenanceDataService _maintenanceDataService;
	private readonly IBinDataServices _binDataService;
	private readonly IDbContextFactory<SapDb> _sapDbFactory;
    private readonly IQCMaintenanceDataService _dataQCMaintenance;
    public FormsAndReportsService(IConfiguration configuration, IQCMaintenanceDataService maintenanceDataService, IBinDataServices binDataServices, IDbContextFactory<SapDb> sapDbFactory, IQCMaintenanceDataService dataQCMaintenance)
    {
        _sql = new MsSqlDataAccess(configuration);
        _maintenanceDataService = maintenanceDataService;
        _binDataService = binDataServices;
        _sapDbFactory = sapDbFactory;
        _dataQCMaintenance = dataQCMaintenance;
    }

    #region Irradiation Label Printing
    public IrradiationLabelPrintingViewModel InitializeIrradiationLabelPrinting()
	{
		try
		{
			IrradiationLabelPrintingViewModel model = new IrradiationLabelPrintingViewModel();

			string qry = $@"select distinct
			A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName"", A.""U_IrridiationDate"" as ""IrradiationDate""
            from ""ORDR"" A 
			inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
            where A.""U_IrridiationDate"" = '{DateTime.Today.ToString("yyyy-MM-dd")}'
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			order by A.""U_IrridiationDate"" desc, A.""DocEntry"" desc
			";
			//AND LOWER(A.""U_SOStatus"") = 'received'";

			model.IrradiationLabelList = _sql.GetData<IrradiationLabel, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			return model;
		}
		catch (Exception)
		{
			throw;
		}

	}

	public void FilterIrradiationSchedule(IrradiationLabelPrintingViewModel model, string start, string end)
	{

		string qry = $@"select distinct
			A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName"", A.""U_IrridiationDate"" as ""IrradiationDate""
            from ""ORDR"" A 
			inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
            where A.""U_IrridiationDate"" BETWEEN '{start}' AND '{end}'
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			order by A.""U_IrridiationDate"" desc, A.""DocEntry"" desc
			";
		//AND LOWER(A.""U_SOStatus"") = 'received'";

		model.IrradiationLabelList = _sql.GetData<IrradiationLabel, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
	}
	#endregion

	#region Sales Order Details
	public IrradiationSalesOrderDetailsViewModel InitilizeSalesOrderDetails(string SONo)
	{
		try
		{
			IrradiationSalesOrderDetailsViewModel model = new IrradiationSalesOrderDetailsViewModel();

			string qry = $@"select distinct
						A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName""
                        , C.""U_ItmSubgroup"" as ""ItemGroup"", A.""U_IrridiationDate"" as ""IrradiationDate""
                        , A.""DocDueDate"" as ""DispatchingDate"", A.""U_PickUpDate"" as ""ReceivingDate""
                        , C.""ItemCode"", C.""U_NoBoxesPallet"" as ""NoBoxesPallet"", B.""Quantity""
            from ""ORDR"" A 
			inner join ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry""
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode""
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode""
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
            where A.""DocEntry"" = {SONo}
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			";

			model.SalesOrderDetail = _sql.FirstOrDefault<IrradiationSalesOrder, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			var QCMaintenance = _maintenanceDataService.GetQCMaintenanceByItem((model.SalesOrderDetail?.ItemCode?.ToString() ?? ""), "Dosimetry Quality Control Order");

			model.SalesOrderDetail.MinDose = QCMaintenance?.ParameterList.Where(x => x.Parameter.ToLower() == "dosimeter" || x.Parameter.ToLower().Contains("dosi") || x.Parameter.ToLower().Contains("dose")).FirstOrDefault()?.MinValue ?? "0";

			model.SalesOrderDetail.MaxDose = QCMaintenance?.ParameterList.Where(x => x.Parameter.ToLower() == "dosimeter" || x.Parameter.ToLower().Contains("dosi") || x.Parameter.ToLower().Contains("dose")).FirstOrDefault()?.MaxValue ?? "0";

			qry = $@"SELECT COUNT(""U_SONo"") ""Count"" FROM OBTN WHERE ""U_SONo"" = {SONo}";
			int Count = _sql.FirstOrDefault<int, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

            LotNo lotNo = new LotNo();
			//for (int x = 1; x <= model.SalesOrderDetail.Quantity; x++)
			for (int x = 1; x <= Count; x++)
			{
				lotNo = new LotNo { IrradiationLotNo = $"{model.SalesOrderDetail.SONo}-{x.ToString("D4")}" };

				model.SalesOrderDetail.IrradiationLotNoList.Add(lotNo);
			}

			return model;
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion

	#region Pallet Label Printing
	public PalletLabelViewModel InitializePalletLabelPrinting()
	{
		try
		{
            string connectionString = _sql.GetConnection("CommonDb");
            string catalog = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

            PalletLabelViewModel model = new PalletLabelViewModel();	

			string qry = $@"select distinct A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName"", A.""DocDueDate"" as ""DispatchDate"", A.""U_PickUpDate"" as ""ReceivingDate""
            from ""ORDR"" A 
			inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
			--inner join ""OITM"" C on B.""U_ItemCode"" = C.""ItemCode""
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
			--inner join ""{catalog}"".""dbo"".""QCOR"" D ON A.""DocEntry"" = D.""DocNo""
            where A.""U_PickUpDate"" = '{DateTime.Today.ToString("yyyy-MM-dd")}'
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'

			AND C.""InvntItem"" = 'Y'
			--AND D.""Status"" = 'Passed'
			--and isnull(B.""U_ItemCode"", '') <> ''
			order by A.""DocEntry"" desc
			";
			//AND LOWER(A.""U_SOStatus"") = 'received'";

			model.PalletLabelList = _sql.GetData<PalletLabel, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			return model;
		}
		catch (Exception)
		{
			throw;
		}

	}

	public void FilterPalletLabel(PalletLabelViewModel model, string start, string end)
	{
        string connectionString = _sql.GetConnection("CommonDb");
        string catalog = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

        string qry = $@"select distinct A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName"", A.""DocDueDate"" as ""DispatchDate"", A.""U_PickUpDate"" as ""ReceivingDate""
            from ""ORDR"" A 
			inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
			--inner join ""OITM"" C on B.""U_ItemCode"" = C.""ItemCode""
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
			inner join ""{catalog}"".""dbo"".""QCOR"" D ON A.""DocEntry"" = D.""DocNo""
            where A.""U_PickUpDate"" BETWEEN '{start}' AND '{end}'
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			AND D.""Status"" = 'Passed'
			--and isnull(B.""U_ItemCode"", '') <> ''
			order by A.""DocEntry"" desc
			";

        //     string qry = $@"select A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName"", A.""DocDueDate"" as ""DispatchDate"", A.""U_PickUpDate"" as ""ReceivingDate""
        //         from ""ORDR"" A 
        //inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
        //--inner join ""OITM"" C on B.""U_ItemCode"" = C.""ItemCode""
        //         inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
        //         where A.""U_PickUpDate"" BETWEEN '{start}' AND '{end}'
        //AND C.""ItmsGrpCod"" = 110 
        //AND C.""InvntItem"" = 'Y'
        //--and isnull(B.""U_ItemCode"", '') <> ''
        //order by A.""DocEntry"" desc
        //";
        //AND LOWER(A.""U_SOStatus"") = 'received'";

        model.PalletLabelList = _sql.GetData<PalletLabel, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
	}
	#endregion

	#region Pallet Label Sales Order Details
	public PalletLabelSalesOrderDetailsViewModel InitilizePalletLabelSalesOrderDetails(string SONo)
	{
		try
		{
			PalletLabelSalesOrderDetailsViewModel model = new PalletLabelSalesOrderDetailsViewModel();

			string qry = $@"select distinct
						A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName""
                        , A.""DocDueDate"" as ""DispatchingDate"", A.""U_PickUpDate"" as ""ReceivingDate""
                        , C.""ItemCode"", CEILING( B.""Quantity"" / C.""U_NoBoxesPallet"") as ""NoBoxesPallet"", B.""Quantity"", C.""U_NoBoxesPallet"" as ""NoBoxes""
            from ""ORDR"" A 
			inner join ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry""
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode""
            inner join ""OITM"" C ON B.""ItemCode"" = C.""ItemCode""
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
            where A.""DocEntry"" = {SONo}
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			";

			model.SalesOrderDetail = _sql.FirstOrDefault<PalletLabelSalesOrder, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			int qty = model.SalesOrderDetail.Quantity;
			int boxes = model.SalesOrderDetail.NoBoxes;
			int totalboxes = 0;

			List<BinAssignment> existingBins = _binDataService.GetPalletLabelPerSO(model.SalesOrderDetail.SONo);

			PalletLabelDetails palletLabel = new PalletLabelDetails();
			using(var db = _sapDbFactory.CreateDbContext())
			for (int x = 1; x <= model.SalesOrderDetail.NoBoxesPallet; x++)
			{
				totalboxes = qty > boxes ? boxes : qty;
				string palletNo = $"{model.SalesOrderDetail.SONo}-{x.ToString("D3")}-{totalboxes}";

				var palletExists = db.OBTN.FirstOrDefault(x => x.MnfSerial == palletNo) is not null;

                palletLabel = new PalletLabelDetails
				{
					PalletNo = palletNo,
					//PalletNo = $"{model.SalesOrderDetail.SONo}-{x.ToString("D4")}-{model.SalesOrderDetail.NoBoxesPallet}",
					NoBoxesPallet = totalboxes,
					BinLocation = existingBins.Where(x => x.PalletNo == palletNo).FirstOrDefault()?.BinCode ?? "",
					IsReceived = palletExists
                    //NoBoxesPallet = model.SalesOrderDetail.NoBoxesPallet
                };

				model.SalesOrderDetail.PalletLabelList.Add(palletLabel);

				qty = qty - boxes;
			}

			qry = $@"select distinct
						A.""DocEntry"" as ""SONo"", A.""CardName"" as ""CustomerName"", C.""ItemName""
                        , A.""DocDueDate"" as ""DispatchingDate"", A.""U_PickUpDate"" as ""ReceivingDate""
                        , C.""ItemCode"", CEILING( B.""Quantity"" / C.""U_NoBoxesPallet"") as ""NoBoxesPallet"", B.""Quantity"", C.""U_NoBoxesPallet"" as ""NoBoxes""
            from ""ORDR"" A 
			inner join ""RDR1"" B ON A.""DocEntry"" = B.""DocEntry""
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode""
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode""
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
			where
			/*C.""ItmsGrpCod"" = 110 */
			TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			";
			//Where LOWER(A.""U_SOStatus"") = 'received'";

			model.SalesOrderList = _sql.GetData<PalletLabelSalesOrder, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			List<BinAssignment> occupiedPallets = _binDataService.GetOccupiedPalletLabel();

			qry = $@"select distinct ""whsCode"" as WarehouseCode, ""sl1code"" as Shelf, ""sl2Code"" as Row, ""sl3code"" as Level, ""sl4code"" as IO from obin where ""sl1code"" <> 'SYSTEM-BIN-LOCATION' and ""sl2code"" <> 'NULL' and ""sl3code"" <> 'NULL' and ""sl4code"" <> 'NULL'";

			model.BinLabelList = _sql.GetData<BinLabelDetails, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			foreach(var binLabel in model.BinLabelList)
			{
				binLabel.BinStatus = occupiedPallets.Where(x => x.BinCode == binLabel.BinCode).Any() ? "Occupied" : "Available"; 
			}

			return model;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public bool SavePalletLabel(List<PalletLabelDetails> selectedPallets, PalletLabelSalesOrder palletLabelSalesOrder)
	{
		try
		{
			List<BinAssignment> model = new List<BinAssignment>();
			foreach (var pallets in selectedPallets)
			{
				model.Add(new BinAssignment { BinCode = pallets.BinLocation, PalletNo = pallets.PalletNo, SONo = palletLabelSalesOrder.SONo, WarehouseCode = pallets.BinLocation.Split("-")[0] });
			}

			_binDataService.SavePalletLabel(model);

			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}
    #endregion

    #region Forms And Reports
	public FormsAndReportsViewModel InitializeFormsAndReports()
	{
		FormsAndReportsViewModel model = new FormsAndReportsViewModel();

        List<DataManager.Models.QCMaintenance.InspectionPlan> inspectionPlans = _dataQCMaintenance.GetQCMaintenance();

        var ItemCodeList = inspectionPlans.DistinctBy(x => x.ItemCode).Select(x => $"'{x.ItemCode}'").ToList();

        string ItemCode = string.Join(", ", ItemCodeList);

        string qry = $@"select distinct
					A.""DocEntry"" as ""SONo""
					, A.""DocNum""
					, CONVERT(VARCHAR,A.""DocDueDate"",101) as ""ReceivingDate""
					, A.""U_PaymentSettlement""
					, A.""CardName"" as ""CustomerName""
					, A.""CardCode"" as ""CustomerCode""
					--, B.""U_ItemCode""  as ""ItemCode""
					, A.""U_CustItems""  as ""ItemCode""
					, C.""ItemName""
					, F.""Quantity"" as ""NoOfBoxes""
					, D.""UgpName"" as ""UoM""
            from ""ORDR"" A 
			inner join ""RDR1"" B on A.""DocEntry"" = B.""DocEntry""
            --inner join ""OITM"" C ON B.""U_ItemCode"" = C.""ItemCode"" 
            inner join ""OITM"" C ON A.""U_CustItems"" = C.""ItemCode"" 
			LEFT JOIN OITB TG ON C.ItmsGrpCod = TG.ItmsGrpCod
			left join ""OUGP"" D ON C.""UgpEntry"" = D.""UgpEntry""
			outer apply (select TOP 1 CAST(ROUND(D.""Quantity"", 0) AS INT) as ""Quantity""
													from ""RDR1"" D 
													--inner join ""OITM"" E on D.""U_ItemCode"" = E.""ItemCode""
													inner join ""OITM"" E on D.""ItemCode"" = E.""ItemCode""
													LEFT JOIN OITB xTG ON E.ItmsGrpCod = xTG.ItmsGrpCod
													where D.""DocEntry"" = A.""DocEntry"" 
													/*AND E.""ItmsGrpCod"" = 110 */
													AND xTG.ItmsGrpNam like '%customer items%'
													AND E.""InvntItem"" = 'Y'
													--and isnull(D.""U_ItemCode"", '') <> ''
													) F
			--outer apply (select DATEADD(SECOND, FLOOR((A.""U_IrridiationEnd""/100)*60)*60 + (A.""U_IrridiationEnd""%100)* 60, A.""U_IrridiationDate"") as ""IrradiationDate"") G
            where CONVERT(INT, F.""Quantity"") > 0
			--and A.U_SOStatus = 'Irradiated - In Storage - For QA'
			--and G.""IrradiationDate"" < GETDATE()
			--and B.""U_ItemCode"" IN({(ItemCode == "" ? "''" : ItemCode)})
			and A.""U_CustItems"" IN({(ItemCode == "" ? "''" : ItemCode)})
			/*AND C.""ItmsGrpCod"" = 110 */
			AND TG.ItmsGrpNam like '%customer items%'
			AND C.""InvntItem"" = 'Y'
			--and isnull(B.""U_ItemCode"", '') <> ''
			ORDER BY A.""DocEntry"" DESC
			";

        model.SalesOrderList = _sql.GetData<FormsAndReportsViewModel.SalesOrderDetail, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		qry = $@"
			SELECT 
				T1.ItemCode, T1.ItemName
			FROM
				OITM T1
			LEFT JOIN OITB TG ON T1.ItmsGrpCod = TG.ItmsGrpCod
			WHERE
				/*ItmsGrpCod = 110 */
				TG.ItmsGrpNam like '%customer items%'
			AND
				T1.InvntItem = 'Y'
			ORDER BY
				T1.CreateDate
			DESC
		";

		model.ItemList = _sql.GetData<FormsAndReportsViewModel.FormItem, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		return model;

    }

    #endregion
}
