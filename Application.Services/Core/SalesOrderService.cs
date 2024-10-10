using Application.Hubs;
using Application.Hubs.Repositories;
using Application.Libraries.SAP;
using Application.Libraries.SAP.DB.Models;
using Application.Libraries.SAP.SL;
using Application.Models;
using B1SLayer;
using DataManager.Libraries.Repositories;
using DataManager.Models.Enums;
using Flurl.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Core
{
	public class SalesOrderService : Repositories.GenericService<Document>, ISalesOrderService
	{
		private readonly IMySqlDataAccess _mysql;
		private readonly IConfiguration _conf;
		private readonly IMapper _mapper;
		private readonly IDbContextFactory<SapDb> _sapDbFactory;
		private readonly IServiceLayerDataAccess _serviceLayer;
		private readonly IHubContext<DashboardNotifHub, IDashboardNotificationClient> _dashboardNotifHub;
		public IDashboardNotificationService _eventsService;

		public SalesOrderService(IMySqlDataAccess mysql, IConfiguration conf, IMapper mapper, IDbContextFactory<SapDb> sapDbFactory, IServiceLayerDataAccess serviceLayer, IHubContext<DashboardNotifHub, IDashboardNotificationClient> dashboardNotifHub, IDashboardNotificationService eventsService)
		{
			_mysql = mysql;
			_conf = conf;
			_mapper = mapper;
			_sapDbFactory = sapDbFactory;
			_serviceLayer = serviceLayer;
			_dashboardNotifHub = dashboardNotifHub;
			_eventsService = eventsService;
		}

		public override Document Create(Document data)
		{
			throw new NotImplementedException();
		}

		public override Task<Document> CreateAsync(Document data)
		{
			throw new NotImplementedException();
		}

		public override Document Get(int id)
		{
			var constr = _conf.GetConnectionString("SAP");
			string qry = "SELECT * FROM ORDR WHERE DocEntry = @id";
			var res = _mysql.GetData<ORDR, object>(qry, new { id }, constr, CommandType.Text).SingleOrDefault();

			if (res is null)
				throw new RowNotInTableException();

			try
			{
				var mapped = _mapper.Map<Document>(res);
				return mapped;
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public override List<Document> GetAll()
		{
			var constr = _conf.GetConnectionString("SAP");
			string qry = "SELECT * FROM ORDR";
			var res = _mysql.GetData<ORDR>(qry, constr, CommandType.Text);
			try
			{
				var mapped = _mapper.Map<List<Document>>(res);
				return mapped;
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public override Task<List<Document>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public override Task<Document> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public bool StatusIs(int docNum, string status)
		{
			var constr = _conf.GetConnectionString("SAP");
			string qry = "SELECT * FROM ORDR";
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var res = (from t0 in db.ORDR
						   where t0.DocNum == docNum
						   select t0.U_SOStatus).Single();
				return res.Equals(status, StringComparison.OrdinalIgnoreCase);
			}
		}

		public string GetStatus(int docNum)
		{
			var constr = _conf.GetConnectionString("SAP");
			string qry = "SELECT * FROM ORDR";
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var res = (from t0 in db.ORDR
						   where t0.DocNum == docNum
						   select t0.U_SOStatus).Single();
				return res;
			}
		}

		public int GetDocEntry(int docNum)
		{
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var id = db.ORDR.Where(x => x.DocNum == docNum).Select(x => x.DocEntry).Single();
				return id;
			}
		}

		private int GetBoxPerPallet(int docEntry) // sales order doc entry
		{

			//join t1 in db.OITM on t2.U_ItemCode equals t1.ItemCode
			//where (t0.DocEntry == docEntry && !string.IsNullOrEmpty(t2.U_ItemCode))
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var d = (from t0 in db.ORDR
						 join t2 in db.RDR1 on t0.DocEntry equals t2.DocEntry
						 join t1 in db.OITM on t0.U_CustItems equals t1.ItemCode
						 where (t0.DocEntry == docEntry)
						 select t1.U_NoBoxesPallet).Single();

				//var boxPerPallet = db.OITM.Where(x => x.DocEntry == docEntry).Select(x => x.U_NoBoxesPallet).Single().GetValueOrDefault();
				return d.GetValueOrDefault();
			}
		}

		private int GetSalesOrderBoxesCount(int docEntry)
		{
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var qty = db.RDR1.Where(x => x.DocEntry == docEntry).Select(x => x.Quantity).First().GetValueOrDefault();
				return decimal.ToInt32(qty);
			}
		}

		public List<Pallet> GetPallets(int docNum)
		{
			var docEntry = GetDocEntry(docNum);
			//var document = Get(docEntry);   
			var boxPerPallet = GetBoxPerPallet(docEntry);
			int qty = GetSalesOrderBoxesCount(docEntry);
			//var itemCode = document.U_CustItems;

			var data = Pallet.GeneratePalletSeries(boxPerPallet, qty);
			var pallets = new List<Pallet>();
			foreach (var item in data)
			{
				var pallet = new Pallet($"{docNum}-{item.Key}-{item.Value}", qty);
				pallets.Add(pallet);
			}
			return pallets;
		}

		public ServiceType GetServiceType(int docEntry)
		{
			using (var db = _sapDbFactory.CreateDbContext())
			{
				var query = from t0 in db.ORDR where t0.DocEntry == docEntry select t0.U_ServiceType;
				var data = query.Single();
				var result = EnumExtensions.GetByDescription<ServiceType>(data);
				return result;
			}
		}

		public async Task UpdateSalesOrderStatus(int docNum, string status)
		{
			if (string.IsNullOrWhiteSpace(status)) return;
			try
			{
				using (var db = _sapDbFactory.CreateDbContext())
				{
					var docEntry = db.ORDR.Where(x => x.DocNum == docNum).Select(x => x.DocEntry).Single();
					await _serviceLayer.PatchAsync("Orders", docEntry, new { U_SOStatus = status });
					//var data = _eventsService.Get(docNum);
					//await _dashboardNotifHub.Clients.All.UpdateSalesOrder(data);
				}

			}
			catch (Exception ex) when (ex is SLException || ex is FlurlHttpException)
			{
				throw;
			}
		}
	}
}
