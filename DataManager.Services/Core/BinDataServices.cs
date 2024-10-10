using DataManager.Models.Bins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataManager.Services.Core;

public class BinDataServices : IBinDataServices
{
    readonly Context _context;
    //readonly ILogger _logger;
    readonly AppSettings _appSettings;
    readonly IMySqlDataAccess _mySql;

    public BinDataServices(Context context, AppSettings appSettings, IMySqlDataAccess mySql)
    {
        _context = context;
        _appSettings = appSettings;
        _mySql = mySql;
    }

    #region Bin Mapping
    public BinMapping PostBinMapping(BinMapping model)
    {
        using (var dbTran = _context.Database.BeginTransaction())
        {
            try
            {
                _context.BinMappings.AddRange(model);
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

    public List<BinMapping> GetBinMapping()
    {
        try
        {
            List<BinMapping> model = _context.BinMappings.Include(b => b.BinMappingPins).AsNoTracking().ToList();
            return model;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public BinMapping GetBinMapping(string WarehouseCode, string Shelf)
    {
        try
        {
            if (!_context.BinMappings.Where(x => x.WarehouseCode == WarehouseCode && x.Shelf == Shelf).Any())
            {
                throw new Exception("Context does not exist");
            }
            BinMapping model = _context.BinMappings.Where(x => x.WarehouseCode == WarehouseCode && x.Shelf == Shelf)
                .Include(b => b.BinMappingPins)
                ?.FirstOrDefault() ?? new BinMapping();
            return model;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public BinMapping PatchBinMapping(string WarehouseCode, string Shelf, BinMapping model)
    {
        using (var dbTran = _context.Database.BeginTransaction())
        {
            try
            {
                BinMapping binModel = _context.BinMappings.Where(x => x.WarehouseCode == WarehouseCode && x.Shelf == Shelf).FirstOrDefault();
                binModel.ImageName = model.ImageName;
                binModel.UpdateDate = DateTime.Now;
                binModel.BinMappingPins = model.BinMappingPins;
                _context.Entry(binModel).State = EntityState.Modified;

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
    #endregion

    #region BinLabelPrinting

    #endregion

    #region Bin Assignment (Pallet Label Printing)
    public List<BinAssignment> SavePalletLabel(List<BinAssignment> model)
    {
        using (var dbTran = _context.Database.BeginTransaction())
        {
            try
            {
                foreach (var bin in model)
                {
                    if (_context.BinAssignments.Where(x => x.BinCode == bin.BinCode).Any())
                    {
                        //CHECK IF THE ASSIGNING BIN IS ALREADY IN OTHER PALLET
                        var bincheck = _context.BinAssignments.Where(x => x.PalletNo == bin.PalletNo).FirstOrDefault();
                        if (bincheck != null)
                        {
                            if (!model.Where(x => x.BinCode == bincheck.BinCode).Any())
                            {
                                //REMOVE Pallet Label in Bin Code
                                bincheck.UpdateDate = DateTime.Now;
                                bincheck.SONo = bin.SONo;
                                bincheck.PalletNo = "";
                                bincheck.WarehouseCode = bin.WarehouseCode;
                                _context.Entry(bincheck).State = EntityState.Modified;

                                //throw new Exception("Assigning an existing bin to another pallet without updating the previous pallet will cause duplication of assigned bin");
                            }
                        }

                        //UPDATE IF EXISTING
                        var newbin = _context.BinAssignments.Where(x => x.BinCode == bin.BinCode).FirstOrDefault();
                        newbin.UpdateDate = DateTime.Now;
                        newbin.SONo = bin.SONo;
                        newbin.PalletNo = bin.PalletNo;
                        newbin.WarehouseCode = bin.WarehouseCode;
                        _context.Entry(newbin).State = EntityState.Modified;
                    }
                    else
                    {
                        //CHECK IF THE ASSIGNING BIN IS ALREADY IN OTHER PALLET
                        var bincheck = _context.BinAssignments.Where(x => x.PalletNo == bin.PalletNo).FirstOrDefault();
                        if (bincheck != null)
                        {
                            if (!model.Where(x => x.BinCode == bincheck.BinCode).Any())
                            {
                                //REMOVE Pallet Label in Bin Code
                                bincheck.UpdateDate = DateTime.Now;
                                bincheck.SONo = bin.SONo;
                                bincheck.PalletNo = "";
                                bincheck.WarehouseCode = bin.WarehouseCode;
                                _context.Entry(bincheck).State = EntityState.Modified;

                                //throw new Exception("Assigning an existing bin to another pallet without updating the previous pallet will cause duplication of assigned bin");
                            }
                        }

                        //SAVE
                        bin.CreateDate = DateTime.Now;
                        _context.BinAssignments.Add(bin);
                    }

                    _context.SaveChanges();
                }

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

    public List<BinAssignment> GetPalletLabelPerSO(string SONo)
    {
        try
        {
            return _context.BinAssignments.Where(x => x.SONo == SONo && x.BinCode != "").ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<BinAssignment> GetOccupiedPalletLabel()
    {
        try
        {
            return _context.BinAssignments.Where(x => x.PalletNo != "").ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public bool ClearPalletLabel(List<string> Pallets)
    {
        using (var dbTran = _context.Database.BeginTransaction())
        {
            try
            {
                foreach (var Pallet in Pallets)
                {
                    var newbin = _context.BinAssignments.Where(x => x.PalletNo == Pallet).ToList();
                    foreach (var bin in newbin)
                    {
                        bin.SONo = "";
                        bin.PalletNo = "";
                        bin.UpdateDate = DateTime.Now;
                        _context.Entry(bin).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }

                dbTran.Commit();
                return true;
            }
            catch (Exception)
            {
                dbTran.Rollback();
                throw;
            }
        }
    }

    public bool BinOccupied(string BinCode, string PalletCode)
    {
        return _context.BinAssignments.Where(x => x.BinCode == BinCode && (x.PalletNo != "" && x.PalletNo != PalletCode)).Any();
	}
    #endregion
}
