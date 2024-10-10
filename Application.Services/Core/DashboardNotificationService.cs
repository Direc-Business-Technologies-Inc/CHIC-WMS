using Application.Libraries.SAP;
using Application.Libraries.SAP.ServiceLayer;
using Application.Libraries.SAP.SL;
using Application.Models.Models;
using B1SLayer;
using DataManager.Libraries.Repositories;
using DataManager.Models.DashboardNotification;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.ViewModels.DashboardNotificationViewModel;
using static DataManager.Models.DashboardNotification.DashboardNotificationModel;

namespace Application.Services.Core
{
    public class DashboardNotificationService : Repositories.GenericService<DashboardNotificationViewModel>, IDashboardNotificationService
    {
        private readonly IDashboardNotificationDataService _dashboardNotificationDataService;
        private readonly IMapper _mapper;
        private readonly IServiceLayerDataAccess _sLDataAccess;
        private readonly IDbContextFactory<SapDb> _sapDbFactory;
		private readonly IConfiguration _conf;
		private readonly IMySqlDataAccess _mysql;

		public DashboardNotificationService(IDashboardNotificationDataService dashboardNotificationDataService, IMapper mapper, IServiceLayerDataAccess sLDataAccess, IDbContextFactory<SapDb> sapDbFactory, IConfiguration conf, IMySqlDataAccess mysql)
		{
			_dashboardNotificationDataService = dashboardNotificationDataService;
			_mapper = mapper;
			_sLDataAccess = sLDataAccess;
			_sapDbFactory = sapDbFactory;
			_conf = conf;
			_mysql = mysql;
		}

		public override DashboardNotificationViewModel Get(int id)
        {
            var result = _dashboardNotificationDataService.Get(id);
            var mapped = _mapper.Map<DashboardNotificationViewModel>(result);
            return mapped;
        }

        public override List<DashboardNotificationViewModel> GetAll()
        {
            var result = _dashboardNotificationDataService.GetAll();
            var mapped = _mapper.Map<List<DashboardNotificationViewModel>>(result); 
            return mapped;
        }

        public override DashboardNotificationViewModel Create(DashboardNotificationViewModel data)
        {
            throw new NotImplementedException();
        }

        public override Task<List<DashboardNotificationViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<DashboardNotificationViewModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<DashboardNotificationViewModel> CreateAsync(DashboardNotificationViewModel data)
        {
            throw new NotImplementedException();
        }

        public async Task<DashboardNotificationViewModel> UpdateAsync(DashboardNotificationViewModel data)
        {

            return data;
        }

		public async Task<DashboardNotificationViewModel> SetOngoingAsync(string palletCode, string status)
		{
			using(var db = _sapDbFactory.CreateDbContext())
            {
                var batches = db.OBTN.Where(x => x.MnfSerial == palletCode);
                if (batches.Count() > 0)
                {
        //            try
        //            {
        //                List<SLBatchRequest> requests = new();

        //                foreach(var batch in batches)
        //                {
        //                    var request = new SLBatchRequest(HttpMethod.Patch, $"BatchNumberDetails({batch.AbsEntry})", new
        //                    {
								//U_TIMS_OngoingStatus = status
        //                    }, requests.Count + 1);
        //                    requests.Add(request);
        //                }

        //                var resp = await _sLDataAccess.BatchAsync(requests.ToArray());
        //                if(resp.FirstOrDefault(x => !x.IsSuccessStatusCode) is HttpResponseMessage err)
        //                {
        //                    var content = await err.Content.ReadAsStringAsync();
        //                    var parsedContent = JsonConvert.DeserializeObject<SLResponseError>(content);
						  //  throw new Exception(parsedContent.Error.Message.Value);
        //                }
        //            } catch( Exception ex) { }
                    
                    var docNum = batches.First().U_SONo;
                    if(docNum.HasValue)
                    {
                        var d = Get(docNum.Value);
                        return d;
                    }
                }
                return new();
			}
		}

		public async Task<bool> UpdateEBStatusSO(DashboardNotificationViewModel Item, string Status)
        {
			var requests = new List<SLBatchRequest>();

			var Order = new Document
			{
				U_EBStatus = Status
			};

			SLBatchRequest orderStatusUpdate = new(HttpMethod.Patch, $"Orders({Item.DocEntry})", Serialize(Order), contentID: requests.Count + 1);
			requests.Add(orderStatusUpdate);

			var resps = await _sLDataAccess.BatchAsync(requests.ToArray());
			var errors = resps.Where(x => !x.IsSuccessStatusCode);
			if (errors.Any())
			{
				var resp = errors.First();
				var error = await resp.Content.ReadAsStringAsync();
				throw new Exception(error);
			}

			return true;
		}

		public async Task<bool> UpdateEBStatusPallet(DashboardNotificationLineViewModel Item, string Status)
		{
			var requests = new List<SLBatchRequest>();

			//Get all batches with same pallet no.
			string query = @$"
					SELECT 
						AbsEntry
					FROM
						OBTN
					WHERE
						MnfSerial = '{Item.PalletNo}'
				";

			var constr = _conf.GetConnectionString("SAP");
			var PalletList = _mysql.GetData(query, constr, System.Data.CommandType.Text);

			foreach (var palletLine in PalletList)
			{
				var batchPayload = new BatchNumberDetail
				{
					U_TIMS_OngoingStatus = Status
				};

				SLBatchRequest batchStatusUpdate = new(HttpMethod.Patch, $"BatchNumberDetails({palletLine.AbsEntry})", Serialize(batchPayload), contentID: requests.Count + 1);
				requests.Add(batchStatusUpdate);
			}

			var resps = await _sLDataAccess.BatchAsync(requests.ToArray());
			var errors = resps.Where(x => !x.IsSuccessStatusCode);
			if (errors.Any())
			{
				var resp = errors.First();
				var error = await resp.Content.ReadAsStringAsync();
				throw new Exception(error);
			}

			return true;
		}

		public async Task<List<DashboardNotificationViewModel>> GetAllSODashboardMobile()
		{
			var result = await _dashboardNotificationDataService.GetAllSODashboardMobile();
			var mapped = _mapper.Map<List<DashboardNotificationViewModel>>(result);
			return mapped;
		}
		public async Task<bool> UpdateStatusSO(DashboardNotificationViewModel Item, string Status)
		{
			var requests = new List<SLBatchRequest>();

			var Order = new Document
			{
				U_SOStatus = Status
			};

			SLBatchRequest orderStatusUpdate = new(HttpMethod.Patch, $"Orders({Item.DocEntry})", Serialize(Order), contentID: requests.Count + 1);
			requests.Add(orderStatusUpdate);

			var resps = await _sLDataAccess.BatchAsync(requests.ToArray());
			var errors = resps.Where(x => !x.IsSuccessStatusCode);
			if (errors.Any())
			{
				var resp = errors.First();
				var error = await resp.Content.ReadAsStringAsync();
				throw new Exception(error);
			}

			return true;
		}
		private string Serialize(object data) => JsonConvert.SerializeObject(data, ServiceLayerExtensions.SerializerSettings);
	}
}
