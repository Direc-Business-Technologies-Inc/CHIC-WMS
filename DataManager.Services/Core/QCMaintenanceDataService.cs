using DataManager.Models.QCMaintenance;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.InteropServices;

namespace DataManager.Services.Core;

public class QCMaintenanceDataService : IQCMaintenanceDataService
{
    readonly Context _context;
    readonly IMySqlDataAccess _sql;
    public QCMaintenanceDataService(Context context, IMySqlDataAccess sql)
    {
        _context = context;
        _sql = sql;
    }
    public InspectionPlan PostQCMaintenance(InspectionPlan model)
    {
        using (var dbTran = _context.Database.BeginTransaction())
        {
            try
            {
                if (model.InspectionPlanCode != "")
                {
                    model.Version = (Convert.ToInt32(model.Version) + 1).ToString();
                }
                else
                {
                    string qry = "SELECT NEXT VALUE FOR MySequence";
                    int nextValue = _sql.FirstOrDefault<int>(qry).Result;
                    model.InspectionPlanCode = $"ISI-TIMS-{(nextValue):0000}";
                }

                foreach (var parameter in model.ParameterList)
                {
                    parameter.InspectionPlanCode = model.InspectionPlanCode;
                    parameter.Version = model.Version;
                }

                _context.InspectionPlans.AddRange(model);
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

    public List<InspectionPlan> GetQCMaintenance()
    {
        try
        {
            string qry = $@"SELECT T1.* FROM [dbo].[QCIP] T1
								where T1.[Version] = (select top 1 
									T2.[Version] 
								from [dbo].[QCIP] T2 
								where 
							T2.InspectionPlanCode = T1.InspectionPlanCode order by T2.[Version] desc)
                            order by T1.[CreateDate] desc";
            //return _context.InspectionPlans.Include(b => b.ParameterList).ToList();
            return _sql.GetData<InspectionPlan, dynamic>(qry, new { }, CommandType.Text);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public InspectionPlan GetQCMaintenance(string InspectionPlanCode)
    {
        try
        {
            return _context.InspectionPlans.Where(x => x.InspectionPlanCode == InspectionPlanCode).Include(b => b.ParameterList).OrderByDescending(x => x.Version).FirstOrDefault();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public InspectionPlan GetQCMaintenance(string ItemCode, string PlanType)
    {
        try
        {
            return _context.InspectionPlans.Where(x => x.ItemCode == ItemCode && x.PlanType == PlanType).Include(b => b.ParameterList).OrderByDescending(x => x.Version).FirstOrDefault();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public InspectionPlan GetQCMaintenanceWithVersion(string InspectionPlanCode, string Version)
    {
        try
        {
            return _context.InspectionPlans.Where(x => x.InspectionPlanCode == InspectionPlanCode && x.Version == Version).Include(b => b.ParameterList).OrderByDescending(x => x.Version).FirstOrDefault();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public InspectionPlan GetQCMaintenanceByItem(string ItemCode, [Optional] string Type)
    {
        try
        {
            if (Type != "")
            {
                return _context.InspectionPlans.Where(x => x.ItemCode == ItemCode && x.PlanType == Type).Include(b => b.ParameterList).OrderByDescending(x => x.Version).FirstOrDefault();
            }
            return _context.InspectionPlans.Where(x => x.ItemCode == ItemCode).Include(b => b.ParameterList).OrderByDescending(x => x.Version).FirstOrDefault();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public List<InspectionPlanParameter> GetInspectionPlanParameters(string InspectionPlanCode, string Version)
    {
        try
        {
            return _context.InspectionPlanParameters.Where(x => x.InspectionPlanCode == InspectionPlanCode && x.Version == Version).ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<InspectionPlan> GetVersionList(string InspectionPlanCode)
    {
        try
        {
            return _context.InspectionPlans.Where(x => x.InspectionPlanCode == InspectionPlanCode)
            .Select(x => new InspectionPlan
            {
                Version = x.Version
            }).ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<ConfigurationItems> GetDefaultParameters()
    {
        try
        {
            return _context.ConfigurationItems.Where(x => x.ConfigurationCode == "QC_MAINTENANCE").ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
