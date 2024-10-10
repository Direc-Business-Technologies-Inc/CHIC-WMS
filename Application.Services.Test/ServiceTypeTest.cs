using Application.Libraries.Mappers;
using Application.Libraries.SAP;
using Application.Services.Core;
using Application.Services.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace Application.Services.Test
{
    public class ServiceTypeTest
    {

        IMapper _mapper = new MapperConfiguration(cfg => {
            cfg.AddMaps(typeof(ServiceTypeMapper));
        }).CreateMapper();

        Mock<IDbContextFactory<SapDb>> _sapDbFactory =new();
        DbContextOptionsBuilder<SapDb> dbOptBuilder = new DbContextOptionsBuilder<SapDb>();
        IServiceTypeService _serviceTypeService;
        public ServiceTypeTest()
        {
            dbOptBuilder.UseSqlServer(Constants.constr_sap);
            _sapDbFactory.Setup(f => f.CreateDbContext())
                .Returns(new SapDb(dbOptBuilder.Options));

            _serviceTypeService = new ServiceTypeService(_sapDbFactory.Object, _mapper);
        }

        [Fact]
        public void GetServiceTypeByCode()
        {
            var s = _serviceTypeService.Get("Storage");
            Assert.NotNull(s);
            Assert.False(string.IsNullOrEmpty(s.Code));
        }


        [Fact]
        public void GetAllServiceType()
        {
            var s = _serviceTypeService.GetAll();
            Assert.NotEmpty(s);
        }
    }
}