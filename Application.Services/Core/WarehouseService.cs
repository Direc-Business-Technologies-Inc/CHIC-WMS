using Application.Libraries.SAP;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Core
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IDbContextFactory<SapDb> _sapDbFactory;
        private readonly IMapper _mapper;

        public WarehouseService(IDbContextFactory<SapDb> sapDbFactory, IMapper mapper)
        {
            _sapDbFactory = sapDbFactory;
            _mapper = mapper;
        }

        public Warehouse Create(Warehouse data)
        {
            throw new NotImplementedException();
        }

        public Warehouse Get(string id)
        {
            using (var db = _sapDbFactory.CreateDbContext())
            {
                var whse = db.OWHS.Where(x=>x.WhsCode == id).Single();
                var mapped = _mapper.Map<Warehouse>(whse);
                return mapped;
            }
        }

        public List<Warehouse> GetAll()
        {
            using(var db = _sapDbFactory.CreateDbContext())
            {
                var whses = db.OWHS.ToList();
                var mapped = _mapper.Map<List<Warehouse>>(whses);
                return mapped;
            }
        }
    }
}
