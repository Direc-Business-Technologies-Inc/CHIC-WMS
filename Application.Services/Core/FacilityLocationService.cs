using DataManager.Libraries.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Core
{
    public class FacilityLocationService : Repositories.GenericService<FacilityLocationViewModel, string>, IFacilityLocationService
    {
        private readonly IMySqlDataAccess _mysql;
        private readonly IMsSqlDataAccess _mssql;

        public FacilityLocationService(IMySqlDataAccess mysql, IMsSqlDataAccess mssql)
        {
            _mysql = mysql;
            _mssql = mssql;
        }

        public override FacilityLocationViewModel Create(FacilityLocationViewModel data)
        {
            throw new NotImplementedException();
        }

        public override Task<FacilityLocationViewModel> CreateAsync(FacilityLocationViewModel data)
        {
            throw new NotImplementedException();
        }

        public override FacilityLocationViewModel Get(string id)
		{
			string constr = _mssql.GetConnection("SAP");
			string qry = """SELECT Code, Name, U_Duration[Duration] FROM "@FACILITYLOCATION" WHERE Code = @id """;
			var res = _mssql.GetData<FacilityLocationViewModel, object>(qry, new {id}, constr, CommandType.Text).Single();
			return res;
		}

        public override List<FacilityLocationViewModel> GetAll()
        {
            string constr = _mssql.GetConnection("SAP");
            string qry = """SELECT Code, Name, U_Duration[Duration] FROM "@FACILITYLOCATION" """;
            var res = _mssql.GetData<FacilityLocationViewModel, object>(qry, null, constr, CommandType.Text);
            return res.ToList();
        }

        public override Task<List<FacilityLocationViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<FacilityLocationViewModel> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
