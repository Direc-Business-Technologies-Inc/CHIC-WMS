using Application.Libraries.SAP;
using Application.Libraries.SAP.SL;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Services.Core
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IDbContextFactory<SapDb> _sapDbFactory;
        private readonly IMapper _mapper;

        public ServiceTypeService(IDbContextFactory<SapDb> sapDb, IMapper mapper)
        {
            _sapDbFactory = sapDb;
            _mapper = mapper;
        }

        public SERVICE_DATA Create(SERVICE_DATA data)
        {
            throw new NotImplementedException();
        }

        public Task<SERVICE_DATA> CreateAsync(SERVICE_DATA data)
        {
            throw new NotImplementedException();
        }

        public SERVICE_DATA Get(string id)
        {
            using(var db = _sapDbFactory.CreateDbContext())
            {
                var query = from t0 in db.SERVICE_DATA
                            where t0.Code == id
                            select t0;
                var result = query.Single();
                var mapped = _mapper.Map<SERVICE_DATA>(result);
                mapped.SERVICE_DATA_ROWCollection = new(GetRows(id));
                return mapped;
            }
        }

        private List<SERVICE_DATA_ROW> GetRows(string id)
        {
            using (var db = _sapDbFactory.CreateDbContext())
            {
                var query = from t0 in db.SERVICE_DATA_ROW
                            orderby t0.U_SortCode ascending
                            where t0.Code == id
                            select t0;
                var result = query.ToList();
                var mapped = _mapper.Map<List<SERVICE_DATA_ROW>>(result);

                return mapped;
            }
        }

        public List<SERVICE_DATA> GetAll()
        {
            using (var db = _sapDbFactory.CreateDbContext())
            {
                var query = from t0 in db.SERVICE_DATA
                            join t1 in db.SERVICE_DATA_ROW on t0.Code equals t1.Code
                            orderby t1.U_SortCode ascending
                            select new { t0, t1 };
                var result = query.ToList();
                Dictionary<string, SERVICE_DATA> map = new();
                result.ForEach(x =>
                {
                    SERVICE_DATA target;
                    if(!map.TryGetValue(x.t0.Code, out target))
                    {
                        SERVICE_DATA newServiceData = _mapper.Map<SERVICE_DATA>(x.t0);
                        
                        map.Add(newServiceData.Code, newServiceData);

                        target = newServiceData;
                    }

                    var newLine = _mapper.Map<SERVICE_DATA_ROW>(x.t1);
                    target.SERVICE_DATA_ROWCollection.Add(newLine);
                });
                return map.Values.OrderBy(x => x.Code).AsList();
            }
        }

        public Task<List<SERVICE_DATA>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SERVICE_DATA> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
