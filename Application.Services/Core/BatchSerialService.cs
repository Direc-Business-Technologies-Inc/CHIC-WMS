using Application.Libraries.SAP;
using Application.Libraries.SAP.DB.Models;
using Application.Models;
using Application.Services.Repositories;
using Dapper;
using DataManager.Libraries.Repositories;
using DataManager.Services.Data;
using Microsoft.EntityFrameworkCore;
using static Application.Models.ViewModels.BatchSerialViewModel;

namespace Application.Services.Core
{
    public  class BatchSerialService: IBatchSerialService
    {
        private readonly IDbContextFactory<SapDb> _sapDbFactory;
        //private readonly IServiceLayerDataAccess _sl;
        //private readonly IBatchSerialService _batchSerialService;
        //private readonly IUtilities _utilities;
        private readonly IConfiguration _conf;
        private readonly IMySqlDataAccess _mysql;
        //private readonly Context _addonDb;
        public BatchSerialService(IDbContextFactory<SapDb> sapDbFactory, IConfiguration conf, IMySqlDataAccess mysql) //, IServiceLayerDataAccess sl, IBatchSerialService batchSerialService, IUtilities utilities, Context addonDb)
        {
            _sapDbFactory = sapDbFactory;
            //_sl = sl;
            //_batchSerialService = batchSerialService;
            //_utilities = utilities;
            _conf = conf;
            _mysql = mysql;
            //_addonDb = addonDb;
        }
       
        public Task<List<BatchSerialViewModel.BatchSerial>> GetBatchSerialByMnfSerialLoc(string itemCode, string mnfSerial, string location)
        {
            try
            {
                var list = new List<BatchSerialViewModel.BatchSerial>();
                using (var db = _sapDbFactory.CreateDbContext())
                {
                    //var qryResult = from t0 in db.OBTQ
                    //                join t1 in db.OITM on t0.ItemCode equals t1.ItemCode
                    //                join t2 in db.OBTN on new { X1 = t0.ItemCode, X2 = t0.SysNumber } equals new { X1 = t2.ItemCode, X2 = t2.SysNumber }
                    //                join t3 in db.OBBQ on new { X1 = t2.ItemCode, X2 = t2.AbsEntry } equals new { X1 = t3.ItemCode, X2 = t3.SnBMDAbs } into j1_lef
                    //                from j1 in j1_lef.DefaultIfEmpty()
                    //                join t4 in db.OBIN on new { X1 = j1.BinAbs } equals new { X1 = t4.AbsEntry } into j2_lef
                    //                from j2 in j2_lef.DefaultIfEmpty()
                    //                where t0.ItemCode == itemCode && t2.MnfSerial == mnfSerial && t0.WhsCode == location && t0.Quantity > 0
                    //                select new { t0.Quantity, t1.ItemCode, t1.ItemName, t2.AbsEntry, t2.DistNumber, t2.MnfSerial, t2.LotNumber, j1.BinAbs, j1.OnHandQty, j2.BinCode };
                    //var data1 = qryResult.ToList();
                    //list = data.Select(result => new BatchSerialViewModel.BatchSerial
                    //{
                    //    AbsEntry = result.AbsEntry,
                    //    DistNumber = result.DistNumber,
                    //    MnfSerial = result.MnfSerial,
                    //    LotNumber = result.LotNumber,
                    //    Quantity = result.Quantity,
                    //    ItemCode = result.ItemCode,
                    //    BinAbs = result.BinAbs,
                    //    BinCode = result.BinCode,
                    //    OnHandQty = result.OnHandQty,
                    //    Location = location,
                    //}).ToList();

                    //string query = $@"select t0.Quantity, T1.ItemCode, t1.ItemName , t2.AbsEntry , t2.DistNumber, t2.MnfSerial , t2.LotNumber, isnull(t3.BinAbs,0) as BinAbs, isnull(t3.OnHandQty,0) as OnHandQty, t4.BinCode 
                    //                    from OBTQ t0 inner join OITM t1 on t0.ItemCode = t1.ItemCode
			                 //                       inner join OBTN  t2 on t0.ItemCode = t2.ItemCode and t0.SysNumber = t2.SysNumber
			                 //                       left join OBBQ t3 on t2.ItemCode = t3.ItemCode and t2.AbsEntry = t3.SnBMDAbs
			                 //                       left join OBIN t4 on t3.BinAbs = t4.AbsEntry
                    //                    where t0.ItemCode = '{itemCode}' and t2.MnfSerial = '{mnfSerial}' and t0.WhsCode ='{location}' and t0.Quantity > 0";

                    string query = $@"WITH RankedOBBQ AS (
                                            SELECT
                                                a.*,
                                                ROW_NUMBER() OVER (PARTITION BY a.SnBMDAbs, a.ItemCode ORDER BY a.AbsEntry DESC) AS RowNum
                                            FROM OBBQ a
                                        )
                                        SELECT
                                            t0.Quantity,
                                            T1.ItemCode,
                                            t1.ItemName,
                                            t2.AbsEntry,
                                            t2.DistNumber,
                                            t2.MnfSerial,
                                            t2.LotNumber,
                                            ISNULL(t3.BinAbs, 0) AS BinAbs,
                                            ISNULL(t3.OnHandQty, 0) AS OnHandQty,
                                            t4.BinCode
                                        FROM OBTQ t0
                                        INNER JOIN OITM t1 ON t0.ItemCode = t1.ItemCode
                                        INNER JOIN OBTN t2 ON t0.ItemCode = t2.ItemCode AND t0.SysNumber = t2.SysNumber
                                        LEFT JOIN RankedOBBQ t3 ON t2.AbsEntry = t3.SnBMDAbs AND t2.ItemCode = t3.ItemCode AND t3.RowNum = 1
                                        LEFT JOIN OBIN t4 ON t3.BinAbs = t4.AbsEntry
                                        WHERE t0.ItemCode = '{itemCode}' AND t2.MnfSerial = '{mnfSerial}' AND t0.WhsCode = '{location}' AND t0.Quantity > 0;";

                    var constr = _conf.GetConnectionString("SAP");
                    list = _mysql.GetData(query, constr, CommandType.Text).Select(result => new  BatchSerialViewModel.BatchSerial
                    {
                        AbsEntry = result.AbsEntry,
                        DistNumber = result.DistNumber,
                        MnfSerial = result.MnfSerial,
                        LotNumber = result.LotNumber,
                        Quantity = result.Quantity,
                        ItemCode = result.ItemCode,
                        BinAbs = result.BinAbs,
                        BinCode = result.BinCode,
                        OnHandQty = result.OnHandQty,
                        Location = location,
                    }).ToList();


                }
                return Task.FromResult(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<BatchSerialViewModel.BatchSerial>> GetBatchSerialByMnfSerialLocUdf(string itemCode, string mnfSerial, string location, string udfName)
        {
            var list = new List<BatchSerialViewModel.BatchSerial>();
            return Task.FromResult(list);
        }

        public Task<List<BinViewModel.BinMappingViewModel.BinAccumulator>> GetBinByBatchSerialLoc(string itemCode, string distNumber, string location)
        {
            try
            {
                var list = new List<BinViewModel.BinMappingViewModel.BinAccumulator>();
                using (var db = _sapDbFactory.CreateDbContext())
                {
                    var qryResult = from t2 in db.OBTN 
                                    join t3 in db.OBBQ on new { X1 = t2.ItemCode, X2 = t2.AbsEntry } equals new { X1 = t3.ItemCode, X2 = t3.SnBMDAbs } into j1_lef
                                    from j1 in j1_lef.DefaultIfEmpty()
                                    join t4 in db.OBIN on new { X1 = j1.BinAbs } equals new { X1 = t4.AbsEntry } into j2_lef
                                    from j2 in j2_lef.DefaultIfEmpty()
                                    where t2.ItemCode == itemCode && t2.DistNumber == distNumber && j1.WhsCode == location && j1.OnHandQty > 0
                                    select new { j1.AbsEntry, j1.BinAbs, j2.BinCode, j1.OnHandQty, j1.WhsCode, j1.SnBMDAbs};
                    var data = qryResult.ToList();
                    list = data.Select(result => new BinViewModel.BinMappingViewModel.BinAccumulator
                    {
                        AbsEntry = result.AbsEntry,
                        BinAbs = result.BinAbs,
                        BinCode = result.BinCode,
                        OnHandQty = result.OnHandQty??0,
                        WhsCode = result.WhsCode,
                        SnBMDAbs = result.SnBMDAbs,
                    }).ToList();
                }
                return Task.FromResult(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<List<BinViewModel.BinMappingViewModel.BinAccumulator>> GetBinByLoc(string location)
        {
            try
            {
                var list = new List<BinViewModel.BinMappingViewModel.BinAccumulator>();
                using (var db = _sapDbFactory.CreateDbContext())
                {
                    var qryResult = from t0 in db.OBIN
                                    where t0.WhsCode == location && t0.Disabled == "N"
                                    orderby t0.SysBin, t0.SL1Abs, t0.SL2Abs, t0.SL3Abs, t0.SL4Abs
                                    select new { t0.AbsEntry, t0.BinCode };
                    var data = qryResult.ToList();
                    list = data.Select(result => new BinViewModel.BinMappingViewModel.BinAccumulator
                    {
                        BinAbs = result.AbsEntry,
                        BinCode = result.BinCode,                        
                    }).ToList();
                }
                return Task.FromResult(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
