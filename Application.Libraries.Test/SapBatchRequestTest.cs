using Application.Libraries.SAP.SL;
using Application.Libraries.Test.POCOs;
using Application.Libraries.Utilies.Newtonsoft;
using B1SLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Application.Libraries.Test
{
    public class SapBatchRequestTest
    {
        private readonly ITestOutputHelper output;

        public SapBatchRequestTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task BatchRequest()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new SapPropertyContractResolver(),
                Converters = new JsonConverter[]
            {
                new SapConverters()
            }
            };

            var uri = new Uri("https://192.168.2.35:50000/b1s/v2");
            SLCredentials creds = new SLCredentials
            {
                CompanyDB = "SBOTEST_ISI",
                Username = "directc_(isi)",
                Password = "1234"
            };
            SLConnection sl = new(uri, creds.CompanyDB, creds.Username, creds.Password);
            var sapsl = new ServiceLayer(uri);
            
            sapsl.Login(creds.CompanyDB, creds.Username, creds.Password, "3").GetValue();

            var order = sapsl.Orders.First();

            SLBatchRequest req1 = new(HttpMethod.Post, "InventoryGenEntries", JsonConvert.SerializeObject(new {
                U_SONo = order.DocNum,
                DocumentLines = new List<DocumentLine>
                {
                    new()
                    {
                        ItemCode = order.U_CustItems,
                        Quantity = 1,
                        BatchNumbers = new System.Collections.ObjectModel.Collection<SAP.SL.BatchNumber>
                        {
                            new()
                            {
                                BatchNumberProperty = "T-0000000001",
                                Quantity = 1,
                            }
                        }
                    }
                }
            }), contentID: 1);

            SLBatchRequest req2 = new(HttpMethod.Patch, $"Orders({order.DocNum})", JsonConvert.SerializeObject(new
            {
                U_SOStatus = "Received - For Storage"
            }), contentID: 2);

            HttpResponseMessage[] resp =  await sl.PostBatchAsync(req1, req2);

            foreach (HttpResponseMessage r in resp)
            {
                var s = await r.Content.ReadAsStringAsync();
                //output.WriteLine(s);
            }
                output.WriteLine(order.DocEntry.ToString());
            Assert.True(resp.All(x=>x.IsSuccessStatusCode));
        }
    }
}
