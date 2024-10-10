using Application.Libraries.SAP.SL;
using Application.Libraries.Test.POCOs;
using Application.Libraries.Utilies.Newtonsoft;
using B1SLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using Application.Libraries.SAP.ServiceLayer;
using Azure.Core;

namespace Application.Libraries.Test;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SapPropertyContractResolverTest()
    {
        var cr = new SapPropertyContractResolver();
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
        {
            //ContractResolver = cr,
            Converters = new JsonConverter[]
            {
                new SapConverters()
            }
        };

        var st = new JsonSerializerSettings()
        {
            ContractResolver = cr,
            Converters = new JsonConverter[]
            {
                new SapConverters()
            }
        };

        var uri = new Uri("https://192.168.2.35:50000/b1s/v2");

        SLCreds cres = new()
        {
            CompanyDB = "SBOTEST_ISI",
            Username = "directc_(isi)",
            Password = "1234"
        };

        var sl = new ServiceLayer(uri);
        var token = sl.Login(cres.CompanyDB, cres.Username, cres.Password, "3").GetValue();

        var b1sl = new SLConnection(uri, cres.CompanyDB, cres.Username, cres.Password);
        var req = b1sl.Request("StockTransfers", 8).WithJsonSerializerSettings(st);
        StockTransfer resp = await req.GetAsync<StockTransfer>(st);

        Assert.NotNull(resp);
        Assert.True(resp.DocDate.HasValue);
        Assert.True(resp.DocDate.Value.Ticks > 0);

        Assert.False(string.IsNullOrEmpty(resp.StockTransferLines[0].BatchNumbers[0].BatchNumberProperty));

        _output.WriteLine(JsonConvert.SerializeObject(resp, st));
    }

    public class SLCreds
    {
        public string CompanyDB { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    
}