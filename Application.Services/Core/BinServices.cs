using System.Net.NetworkInformation;
using static Application.Models.ViewModels.BinLabelViewModel;

namespace Application.Services.Core;

public class BinServices : IBinServices
{
	private readonly IMapper Mapper;
	private readonly IMsSqlDataAccess _sql;
	private readonly IBinDataServices _binDataServices;

	public BinServices(IConfiguration configuration, IBinDataServices binDataServices, IMapper mapper)
	{
		_sql = new MsSqlDataAccess(configuration);
		_binDataServices = binDataServices;
		Mapper = mapper;
	}

	#region BinMapping
	public BinMappingViewModel InitializeBinMapping(BinMappingViewModel model)
	{
		try
		{
			string qry = $@"select ""WhsCode"" as ""WarehouseCode"", ""WhsName"" as ""WarehouseName"" from OWHS WHERE ""BinActivat"" = 'Y'";

			model.WarehouseList = _sql.GetData<Warehouses, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			//qry = $@"SELECT T0.[SLCode]  as ""Shelf"", 
			//          Count(T1.[BinCode]) as ""Count""
			//          FROM OBSL T0  
			//          INNER JOIN OBIN T1 ON T0.[AbsEntry] = T1.[SL1Abs] 
			//          WHERE T1.[SysBin] = 'N'
			//          GROUP BY T0.[SLCode]
			//          HAVING COUNT(T1.[BinCode]) > 0";

			//model.ShelfList = _sql.GetData<Shelves, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

			model.AisleList = new List<Aisles>
		{
			new Aisles { Aisle = "Inner", Code = "I"},
			new Aisles { Aisle = "Outer", Code = "O"}
		};

			

			return model;
		}
		catch (Exception)
		{
			throw;
		}

	}

	public void SelectInput(BinMappingViewModel model, string InputName, string value)
	{
		model.BinMappingHeader.GetType().GetProperty(InputName).SetValue(model.BinMappingHeader, value);

		if (InputName == "WarehouseCode")
		{
			string WarehouseName = model.WarehouseList.Where(x => x.WarehouseCode == value).FirstOrDefault()?.WarehouseName;
			model.BinMappingHeader.GetType().GetProperty("WarehouseName").SetValue(model.BinMappingHeader, WarehouseName);
		}
	}

	public void GetShelf(BinMappingViewModel model, string WarehouseCode)
	{
		string qry = $@"SELECT DISTINCT T0.[SL1Code] as ""Shelf""
					FROM OBIN T0 
					WHERE T0.WhsCode = '{WarehouseCode}'
					AND T0.[SL1Code] <> 'SYSTEM-BIN-LOCATION'";

		model.ShelfList = _sql.GetData<Shelves, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
	}

	public ICollection<BinMappingPins> AdjustPinList(BinMappingViewModel model, string Method, BinMappingPins Data, float IntrinsicWidth, float RenderedWidth)
	{
		Data.Radius = (IntrinsicWidth * Data.Radius) / RenderedWidth;
		Data.Left = (IntrinsicWidth * Data.Left) / RenderedWidth;
		Data.Top = (IntrinsicWidth * Data.Top) / RenderedWidth;

		var BinPinModel = model.BinMappingHeader.BinMappingPins.Where(x => x.BinCode == Data.BinCode).FirstOrDefault();
		if (Method.ToLower() == "post")
		{
			model.BinMappingHeader.BinMappingPins.Add(Data);
		}

		if (Method.ToLower() == "patch")
		{
			if (BinPinModel != null)
			{
				BinPinModel.WarehouseCode = Data.WarehouseCode;
				BinPinModel.Shelf = Data.Shelf;
				BinPinModel.BinCode = Data.BinCode;
				BinPinModel.Aisle = Data.Aisle;
				BinPinModel.Text = Data.Text;
				BinPinModel.Row = Data.Row;
				BinPinModel.Level = Data.Level;
				BinPinModel.Radius = Data.Radius;
				BinPinModel.Left = Data.Left;
				BinPinModel.Top = Data.Top;

			}
		}

		if (Method.ToLower() == "delete")
		{
			if (BinPinModel != null)
			{
				model.BinMappingHeader.BinMappingPins.Remove(BinPinModel);
			}
		}

		return model.BinMappingHeader.BinMappingPins;
	}

	public void FetchColumn(BinMappingViewModel model)
	{
		string WarehouseCode = model.BinMappingHeader.WarehouseCode;
		string Shelf = model.BinMappingHeader.Shelf;

		//string qry = $@"SELECT T0.[SLCode] as ""Level"", 
		//       Count(T1.[BinCode]) as ""Count""
		//       FROM OBSL T0  
		//       INNER JOIN OBIN T1 ON T0.[SLCode] = T1.[SL3Code] 
		//       WHERE T1.[SysBin] = 'N' AND T0.[FldAbs] = '3'
		//       GROUP BY T0.[SLCode]
		//       HAVING COUNT(T1.[BinCode]) > 0 ORDER BY T0.[SLCode] ASC";

		string qry = $@"SELECT DISTINCT T0.[SL3Code] as ""Level""
					FROM OBIN T0 
					WHERE T0.WhsCode = '{model.BinMappingHeader.WarehouseCode}'
					AND T0.SL1Code = '{model.BinMappingHeader.Shelf}'
					AND ISNULL(T0.[SL3Code], '') <> ''";

		model.LevelList = _sql.GetData<Levels, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		//qry = $@"SELECT T0.[SLCode] as ""Row"", 
		//	        Count(T1.[BinCode]) as ""Count""
		//	        FROM OBSL T0  
		//	        INNER JOIN OBIN T1 ON T0.[SLCode] = T1.[SL2Code] 
		//	        WHERE T1.[SysBin] = 'N' AND T0.[FldAbs] = '2'
		//	        GROUP BY T0.[SLCode]
		//	        HAVING COUNT(T1.[BinCode]) > 0";

		//qry = $@"SELECT DISTINCT T0.[SL2Code] 
		//	FROM OBIN T0 
		//	WHERE T0.WhsCode = '{model.BinMappingHeader.WarehouseCode}' 
		//	AND T0.SL1Code = '{model.BinMappingHeader.Shelf}' 
		//	AND T0.SL3Code = '{model.BinMappingHeader.Level}'
		//	AND T0.SL4Code = '{model.BinMappingHeader.Aisle}'";

		//model.RowList = _sql.GetData<Rows, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		//return model.RowList[0].Count;
	}

	public void FetchRows(BinMappingViewModel model, string Aisle)
	{
		//qry = $@"SELECT T0.[SLCode] as ""Row"", 
		//	        Count(T1.[BinCode]) as ""Count""
		//	        FROM OBSL T0  
		//	        INNER JOIN OBIN T1 ON T0.[SLCode] = T1.[SL2Code] 
		//	        WHERE T1.[SysBin] = 'N' AND T0.[FldAbs] = '2'
		//	        GROUP BY T0.[SLCode]
		//	        HAVING COUNT(T1.[BinCode]) > 0";

		string qry = $@"SELECT DISTINCT T0.[SL2Code] as ""Row""
			FROM OBIN T0 
			WHERE T0.WhsCode = '{model.BinMappingHeader.WarehouseCode}' 
			AND T0.SL1Code = '{model.BinMappingHeader.Shelf}' 
			AND T0.SL3Code = '{model.BinMappingHeader.Level}'
			AND T0.SL4Code = '{Aisle}'";

		model.RowList = _sql.GetData<Rows, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

	}

	public bool SaveBinMapping(BinMappingViewModel model)
	{
		BinMapping binMapping = Mapper.Map<BinMapping>(model.BinMappingHeader);
		try
		{
			if (_binDataServices.GetBinMapping(binMapping.WarehouseCode, binMapping.Shelf) != null)
			{
				//PATCH
				_binDataServices.PatchBinMapping(binMapping.WarehouseCode, binMapping.Shelf, binMapping);
			}

			return true;
		}
		catch (Exception ex)
		{
			if (ex.Message == "Context does not exist")
			{
				//POST
				_binDataServices.PostBinMapping(binMapping);

				return true;
			}
			throw;
		}
	}

	public bool UploadImage(string ImgSrc, string FileName)
	{
		try
		{
			byte[] imageBytes = Convert.FromBase64String(ImgSrc.IndexOf(",") >= 0 ? ImgSrc.Substring(ImgSrc.IndexOf(",") + 1) : "");

			if (imageBytes.Length > 0)
			{
				// Save the byte array as an image file
				File.WriteAllBytes(FileName, imageBytes);
			}
			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public bool GetBinMapping(BinMappingViewModel model)
	{
		try
		{
			BinMapping binMapping = _binDataServices.GetBinMapping(model.BinMappingHeader.WarehouseCode, model.BinMappingHeader.Shelf);
			model.BinMappingHeader = Mapper.Map<BinMappingHeaders>(binMapping);
			return true;
		}
		catch (Exception ex)
		{
			if (ex.Message == "Context does not exist")
			{
				return false;
			}
			throw;
		}
	}

	#endregion

	#region BinLabelPrinting
	
	public List<WarehouseCode> GetWarehousesCodes() {

		/*List<BinMappingPin> binMappingPins = _binDataServices.GetBinLabels();
        *//*List<BinLabel> BinLabels = Mapper.Map<BinLabel>(binMappingPins);*//*

		List<BinLabel> BinLabels = Mapper.Map<List<BinLabel>>(binMappingPins);*/

		try
		{
			string qry = $@"select distinct ""WhsCode"" as ""whsCode"" from obin where ""SL1Code"" <> 'SYSTEM-BIN-LOCATION' order by ""WhsCode""";
			List<WarehouseCode> warehouseCodeList = _sql.GetData<WarehouseCode, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
			foreach (var item in warehouseCodeList)
			{
				if (item.whsCode.StartsWith('C'))
				{
					item.whsName = "Cold Storage";
				}
				else
				{
					item.whsName = "Dry Storage";
				}
			}
			return warehouseCodeList;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public List<BinLabel> GetBinsData(string whsCode)
	{
		string qry = $@"select distinct ""whsCode"" as WarehouseCode, ""sl1code"" as Shelf, ""sl2Code"" as Row, ""sl3code"" as level from obin where ""whscode"" = '{whsCode}' and ""sl1code"" <> 'SYSTEM-BIN-LOCATION' and ""sl2code"" <> 'NULL' and ""sl3code"" <> 'NULL' and ""sl4code"" <> 'NULL' and ""Disabled"" = 'N'";
		return _sql.GetData<BinLabel, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);
    }
    #endregion
}
