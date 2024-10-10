using Microsoft.EntityFrameworkCore;

namespace DataManager.Services.Core;

public class QCOrderDataService : IQCOrderDataService
{
	readonly Context _context;
	readonly IMySqlDataAccess _sql;
	public QCOrderDataService(Context context, IMySqlDataAccess sql)
	{
		_context = context;
		_sql = sql;
	}

	public QCOrder PostQCOrder(QCOrder model)
	{
		using (var dbTran = _context.Database.BeginTransaction())
		{
			try
			{
				string qry = "SELECT NEXT VALUE FOR QCOrderSequence";
				int nextValue = _sql.FirstOrDefault<int>(qry).Result;
				model.QCOrderNo = $"TIMS-QC-{(nextValue):0000}";
				model.CreateDate = DateTime.Now;

				model.QCOrderSampleDetail.QCOrderNo = model.QCOrderNo;
				model.QCOrderDosimetryReport.QCOrderNo = model.QCOrderNo;

				_context.QCOrders.AddRange(model);
				_context.SaveChanges();

				dbTran.Commit();
				return model;
			}
			catch (Exception)
			{
				dbTran.Rollback();
				throw;
			}
		}
	}

	public List<QCOrder> GetQCOrderList()
	{
		return _context.QCOrders.OrderByDescending(x => x.CreateDate).Select(x => new QCOrder
		{
			QCOrderNo = x.QCOrderNo,
			CustomerName = x.CustomerName,
			ItemName = x.ItemName,
			InspectionPlanType = x.InspectionPlanType,
			Status = x.Status,
			DocNo = x.DocNo

		}).ToList();
	}

	public QCOrder GetQCOrder(string QCOrderNo)
	{
		return _context.QCOrders.Where(x => x.QCOrderNo == QCOrderNo)
			.Include(x => x.QCOrderDosimetryReport)
			.Include(x => x.QCOrderSampleDetail)
			.ThenInclude(x => x.QCOrderSampleList)
			.ThenInclude(x => x.QCOrderParameterList)?
			.FirstOrDefault() ?? new QCOrder();
	}
	public QCOrder GetQCOrder(string ItemCode, string PlanType)
	{
		return _context.QCOrders.Where(x => x.ItemCode == ItemCode && x.InspectionPlanType == PlanType)
			.Include(x => x.QCOrderDosimetryReport)
			.Include(x => x.QCOrderSampleDetail)
			.ThenInclude(x => x.QCOrderSampleList)
			.ThenInclude(x => x.QCOrderParameterList)?
			.FirstOrDefault();
	}

	public QCOrder PatchQCOrder(QCOrder model)
	{
		using (var dbTran = _context.Database.BeginTransaction())
		{
			try
			{
				QCOrder qCOrder = _context.QCOrders.Where(x => x.QCOrderNo == model.QCOrderNo)
					.Include(x => x.QCOrderDosimetryReport)
					.Include(x => x.QCOrderSampleDetail)
					.ThenInclude(x => x.QCOrderSampleList)
					.ThenInclude(x => x.QCOrderParameterList)?
					.FirstOrDefault();

				qCOrder.InspectionPlanCode = model.InspectionPlanCode;
				qCOrder.InspectionPlanName = model.InspectionPlanName;
				qCOrder.InspectionPlanType = model.InspectionPlanType;
				qCOrder.CustomerCode = model.CustomerCode;
				qCOrder.CustomerName = model.CustomerName;
				qCOrder.ItemCode = model.ItemCode;
				qCOrder.ItemName = model.ItemName;
				qCOrder.Remarks = model.Remarks;
				qCOrder.DocNo = model.DocNo;
				qCOrder.DocDate = model.DocDate;
				qCOrder.DocType = model.DocType;
				qCOrder.Quantity = model.Quantity;
				qCOrder.UoM = model.UoM;
				qCOrder.RefNo = model.RefNo;
				qCOrder.Status = model.Status;
				qCOrder.SampleSize = model.SampleSize;
				qCOrder.SamplePassTolerancePercentage = model.SamplePassTolerancePercentage;
				qCOrder.OverallPassTolerancePercentage = model.OverallPassTolerancePercentage;
				qCOrder.PONo = model.PONo;
				qCOrder.ManufacturingLotNo = model.ManufacturingLotNo;
				qCOrder.ServiceOrderNo = model.ServiceOrderNo;
				qCOrder.StorageConditions = model.StorageConditions;
				qCOrder.UpdateDate = DateTime.Now;
				qCOrder.QCOrderSampleDetail = model.QCOrderSampleDetail;
				qCOrder.QCOrderDosimetryReport = model.QCOrderDosimetryReport;
				qCOrder.DosimetryUsed = model.DosimetryUsed;

				_context.Entry(qCOrder).State = EntityState.Modified;

				_context.SaveChanges();

				dbTran.Commit();
				return model;
			}
			catch (Exception)
			{
				dbTran.Rollback();
				throw;
			}
		}
	}
}
