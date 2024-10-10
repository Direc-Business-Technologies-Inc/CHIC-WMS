using Application.Libraries.SAP;
using Application.Libraries.SAP.DB.Models;
using Application.Models;
using Dapper;
using DataManager.Libraries.Repositories;
using DataManager.Models.Enums;
using DataManager.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services.Core
{
	public class PalletService : Repositories.GenericService<Pallet, string>, IPalletService
	{
		private readonly IDbContextFactory<SapDb> _sapDbFactory;
		private readonly IConfiguration _conf;
		private readonly IMySqlDataAccess _mysql;
		private readonly Context _addonDb;
		private readonly IServiceTypeService _serviceTypeService;

		public PalletService(IDbContextFactory<SapDb> sapDb, Context addonDb, IConfiguration conf, IMySqlDataAccess mysql, IServiceTypeService serviceTypeService)
		{
			_sapDbFactory = sapDb;
			_addonDb = addonDb;
			_conf = conf;
			_mysql = mysql;
			_serviceTypeService = serviceTypeService;
		}

		public override Pallet Create(Pallet data)
		{
			throw new NotImplementedException();
		}

		public override Task<Pallet> CreateAsync(Pallet data)
		{
			throw new NotImplementedException();
		}

		public override Pallet Get(string id)
		{
			Pallet pallet;

			var (soNum, series, qty) = ExtractPalletDetails(id);

			ORDR so;
			RDR1 soRow;

			using (var db = _sapDbFactory.CreateDbContext())
			{
				so = db.ORDR.Where(x => x.DocNum == soNum).FirstOrDefault();
				if (so is null) throw new Exception($"[SAP] DocNum {soNum} doesn't exist");

				//BLOCK IF THE SO IS NOT YET RECEIVED
				int SONo = Convert.ToInt32(soNum);
				var obtn = db.OBTN.Where(x => x.U_SONo == SONo).FirstOrDefault();
				if (obtn is null) throw new Exception($"[SAP] SO Number {obtn} not yet received");


				//join t1 in db.OITM on t3.U_ItemCode equals t1.ItemCode
				//select new { t0.U_ServiceType, t3.U_ItemCode, t1.ItemName, t3.Quantity, t1.U_NoBoxesPallet }).SingleOrDefault();
				//where (t0.DocEntry == so.DocEntry && !string.IsNullOrEmpty(t3.U_ItemCode))
				var result = (
					from t0 in db.ORDR
					join t3 in db.RDR1 on t0.DocEntry equals t3.DocEntry
					join t1 in db.OITM on t0.U_CustItems equals t1.ItemCode
					where (t0.DocEntry == so.DocEntry)
					select new { t0.U_ServiceType, t0.U_CustItems, t1.ItemName, t3.Quantity, t1.U_NoBoxesPallet }).FirstOrDefault();

				pallet = new Pallet(
					so.DocNum,
					int.Parse(series),
					result.U_NoBoxesPallet.GetValueOrDefault(),
					(int)result.Quantity.GetValueOrDefault()
					);

				////GET MATRIX LAST SORTCODE -KARL 11/28/2023
				//            int matrixLastSortCode = (
				//	from t0 in db.SERVICE_DATA_ROW
				//	where t0.Code == result.U_ServiceType
				//                orderby t0.U_SortCode descending
				//	select t0.U_SortCode ?? -1).Take(1).FirstOrDefault();

				//GET PALLET LAST SORTCODE -KARL 11/28/2023
				//.Where(A => A.T2.T0.ItemCode == result.U_ItemCode &&
				var YourFirstQuery = db.OITL
					.Join(db.ITL1, T0 => new { T0.ItemCode, T0.LogEntry }, T1 => new { T1.ItemCode, T1.LogEntry }, (T0, T1) => new { T0, T1 })
					.Join(db.OBTN, T2 => new { T2.T1.ItemCode, T2.T1.SysNumber }, T1 => new { T1.ItemCode, T1.SysNumber }, (T2, T1) => new { T2, T1 })
					.Where(A => A.T2.T0.ItemCode == result.U_CustItems &&
								A.T1.MnfSerial == id &&
								A.T1.U_SONo == soNum &&
								A.T2.T0.ApplyType == 67 &&
								A.T2.T0.DocQty > 0 &&
								A.T2.T0.LocCode != "NC")
					.OrderByDescending(A => A.T2.T0.LogEntry)
					.AsEnumerable()
					.Select((A, index) => new
					{
						A.T2.T0.LocCode
					})
					.Distinct();


				//.Where(N => N.Z.X.ItemCode == result.U_ItemCode &&
				var YourSecondQuery = YourFirstQuery
					.AsEnumerable()
					.ToList()
					.Select((A, index) => new
					{
						A.LocCode,
						LogEntry = (db.OITL
								.Join(db.ITL1, X => new { X.ItemCode, X.LogEntry }, Y => new { Y.ItemCode, Y.LogEntry }, (X, Y) => new { X, Y })
								.Join(db.OBTN, Z => new { Z.Y.ItemCode, Z.Y.SysNumber }, Y => new { Y.ItemCode, Y.SysNumber }, (Z, Y) => new { Z, Y })
								.Where(N => N.Z.X.ItemCode == result.U_CustItems &&
											N.Y.MnfSerial == id &&
											N.Y.U_SONo == soNum &&
											N.Z.X.ApplyType == 67 &&
											N.Z.X.DocQty > 0 &&
											N.Z.X.LocCode == (A.LocCode))
								.OrderByDescending(N => N.Z.X.LogEntry)
								.AsEnumerable()
								.Select((N) => new
								{
									N.Z.X.LogEntry,
								})
								.Distinct()
								.FirstOrDefault().LogEntry)
					});

				var OrderedResults = YourSecondQuery
					.AsEnumerable()
					.OrderByDescending(A => A.LogEntry)
					.Select((A, index) => new
					{
						A.LocCode,
						A.LogEntry,
						RowPriority = index + 1
					});

				int lastSortCode = OrderedResults
						.AsEnumerable()
						.Select(A => new
						{
							U_SortCode = A.LocCode == ((db.SERVICE_DATA_ROW
														.Where(X => X.U_WarehouseCode == A.LocCode && X.Code == result.U_ServiceType && A.RowPriority == 1)
														.FirstOrDefault() ?? new SERVICE_DATA_ROW()).U_WarehouseCode ?? "")
								? (db.SERVICE_DATA_ROW.Where(X => X.U_WarehouseCode == A.LocCode && X.Code == result.U_ServiceType).FirstOrDefault() ?? new SERVICE_DATA_ROW()).U_SortCode ?? 0
								: ((db.SERVICE_DATA_ROW.Where(X => X.U_WarehouseCode == A.LocCode && X.Code == result.U_ServiceType && A.RowPriority == 2).FirstOrDefault() ?? new SERVICE_DATA_ROW()).U_SortCode ?? 0) + 1
						})
						.OrderByDescending(A => A.U_SortCode)
						.FirstOrDefault()?.U_SortCode ?? 0;

				//int lastSortCode = (
				//    from t0 in db.OBTN
				//    where t0.MnfSerial == id
				//    && t0.U_SONo == soNum
				//    orderby t0.U_TIMS_ITSortCode descending
				//    select t0.U_TIMS_ITSortCode ?? 0).Take(1).FirstOrDefault();

				var actualQuantity = (
					from t0 in db.OBTN
					where t0.MnfSerial == id
					&& t0.U_SONo == soNum
					&& (t0.U_TIMS_ITSortCode ?? 0) == lastSortCode
					select t0.MnfSerial).Count();

				var d = (from t0 in db.OBTN
						 where t0.MnfSerial == id
						 select t0).ToList();
				var dg = d.GroupBy(x => x.U_BatchStatus);
				var mdg = dg.MaxBy(x => x.Count());
				pallet.DisplayStatus =
					mdg?.Key ?? "";

				pallet.ActualQuantity = actualQuantity;


				//GET PALLET DISPLAY STATUS -KARL 12/05/2023
				pallet.DisplayStatus = (
					from t0 in db.OBTN
					where t0.MnfSerial == id
					&& t0.U_SONo == soNum
					select t0.U_BatchStatus ?? "").Take(1).FirstOrDefault();

				pallet.ItemName = result.ItemName;
				pallet.ItemCode = result.U_CustItems;
				//pallet.ItemCode = result.U_ItemCode;
				pallet.ServiceType = result.U_ServiceType;

				if (pallet != null)
				{
					#region GET_ADDON_PALLET_ASSIGNMENT
					OBIN bin = new OBIN();
					var binAssigned = _addonDb.BinAssignments.Where(x => x.PalletNo == pallet.Code).FirstOrDefault();
					if (binAssigned is not null)
					{
						pallet.BinCode = binAssigned.BinCode;
						bin = db.OBIN.Where(x => x.BinCode == pallet.BinCode).SingleOrDefault();

						if (bin is null)
						{
							throw new Exception($"[SAP] BinCode: {pallet.BinCode} is not defined");
						}
						pallet.BinEntry = bin.AbsEntry;
					}
					else
						if (lastSortCode != 3 && lastSortCode != 4 && lastSortCode != 5)
						throw new Exception($"[SAO] Bin assinment not found! Create first a bin assigment. Pallet No {pallet.Code}");
					#endregion //PALLET_ASSIGNMENT                    
				}
			}

			var boxes = GetPalletBatches(pallet.Code, pallet.ServiceType);
			pallet.Boxes = boxes;

			return pallet;
		}

		public override List<Pallet> GetAll()
		{
			throw new NotImplementedException();
		}

		public override Task<List<Pallet>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public override Task<Pallet> GetAsync(string id)
		{
			throw new NotImplementedException();
		}

		public (int soNum, string palletSeries, int maxBoxNo) ExtractPalletDetails(string palletCode)
		{
			string pal;
			int box, so;

			if (string.IsNullOrEmpty(palletCode)) throw new ArgumentNullException("palletCode");

			string[] extractedData = palletCode.Split('-');
			if (extractedData.Length < 3) throw new InvalidDataException();

			int.TryParse(extractedData[0], out so);
			pal = extractedData[1];

			string strBoxNo = extractedData[2];
			int.TryParse(strBoxNo, out box);

			return (so, pal, box);
		}

		private List<Box> GetPalletBatches(string palletCode, string serviceType)
		{
			using (var db = _sapDbFactory.CreateDbContext())
			{
				//GET MATRIX LAST SORTCODE -KARL 11/28/2023
				int matrixLastSortCode = (
					from t0 in db.SERVICE_DATA_ROW
					where t0.Code == serviceType
					orderby t0.U_SortCode descending
					select t0.U_SortCode ?? -1).Take(1).FirstOrDefault();

				//GET PALLET LAST SORTCODE -KARL 11/28/2023
				int lastSortCode = (
					from t0 in db.OBTN
					join t1 in db.OITM on t0.ItemCode equals t1.ItemCode
					where t0.MnfSerial == palletCode
					orderby t0.U_TIMS_ITSortCode ?? 0 descending
					select t0.U_TIMS_ITSortCode ?? 0).Take(1).FirstOrDefault();
				//&& t0.U_TIMS_ITSortCode != matrixLastSortCode

				var qryResult = from t0 in db.OBTN
								join t1 in db.OITM on t0.ItemCode equals t1.ItemCode
								where t0.MnfSerial == palletCode
								&& (t0.U_TIMS_ITSortCode ?? 0) == lastSortCode //GET ACTUAL QUANTITY OF BOXES WITH LATEST HIGHEST SORT CODE EXCEPT LAST SORT CODE -KARL 11/28/2023
								select new { t0.DistNumber, t0.MnfSerial, t0.U_SONo, t0.U_BatchStatus };
				var data = qryResult.ToList();
				var mapped = data.Select(result => new Box
				{
					Id = result.DistNumber,
					PalletCode = result.MnfSerial,
					Quantity = 1,
					SalesOrderDocNum = result.U_SONo,
					Status = result.U_BatchStatus
				}).AsList();

				return mapped;
			}
		}

		public bool IsForRelease(string palletCode)
		{
			var pallet = Get(palletCode);
			var serviceType = _serviceTypeService.Get(pallet.ServiceType);
			var lastTransferType = serviceType.SERVICE_DATA_ROWCollection.Last();
			var isForRelease = lastTransferType.U_DisplayStatus == pallet.DisplayStatus;
			//var isForRelease = lastTransferType.Code == pallet.DisplayStatus;
			return isForRelease;
		}
		public async Task<string> GetIsExistBincode(string palletCode, string binCode)
		{
			return _addonDb.BinAssignments
					.Where(x => x.PalletNo == palletCode && x.BinCode == binCode)
					.Select(x => x.BinCode).FirstOrDefault();

		}
	}
}
