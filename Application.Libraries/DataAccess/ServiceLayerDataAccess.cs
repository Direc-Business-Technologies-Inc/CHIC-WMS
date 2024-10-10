using Application.Libraries.Utilies.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Application.Libraries.SAP.ServiceLayer;
using System.Runtime.InteropServices;

namespace Application.Libraries.DataAccess;

public class ServiceLayerDataAccess : IServiceLayerDataAccess
{
	//SLConnection _serviceLayer = new SLConnection("https://10.0.140.119:50000/b1s/v1", "SBOTEST_ISI_09082023", "directc_(isi)", "1234");
	//SLConnection _serviceLayer = new SLConnection("https://192.168.2.35:50000/b1s/v1", "SBOTEST_ISI", "directc_(isi)", "1234");
	private SLConnection _serviceLayer;
    private readonly JsonSerializerSettings serializerSettings;
    private readonly IConfiguration _configuration;
	private readonly SapServiceLayerLogin _creds;
    public ServiceLayerDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;

		var _creds = _configuration.GetSection("SapServiceLayer").Get<SapServiceLayerLogin>();

	
        _serviceLayer = new SLConnection(_creds.Uri, _creds.CompanyDB, _creds.UserName, _creds.Password);

		serializerSettings = ServiceLayerExtensions.SerializerSettings;


    }

    public async void Logout()
	{
		// Performs a POST on /Logout, ending the current session
		await _serviceLayer.LogoutAsync();
	}

	public async Task<HttpResponseMessage[]> BatchAsync(SLBatchRequest[] sLBatchRequests)
	{
		try
		{
			HttpResponseMessage[] batchResult = await _serviceLayer.PostBatchAsync(sLBatchRequests);
			Logout();
			return batchResult;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<T> PostAsync<T, U>(string Module, U data)
	{
		try
		{
			var createdOrder = await _serviceLayer.Request(Module).PostAsync<T>(data);
			Logout();
			return createdOrder;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<U> PatchAsync<U>(string Module, dynamic Id, U data)
	{
		try
		{
			await _serviceLayer.Request(Module, Id).PatchAsync(data);
			Logout();
			return data;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<string> PatchStringAsync(string Module, dynamic Id, string data)
	{
		try
		{
			await _serviceLayer.Request(Module, Id).PatchStringAsync(data);
			Logout();
			return data;
		}
		catch (Exception)
		{
			throw;
		}
	}

    public Task<T> GetAsync<T>(string module)
    {
		try
		{
            var request = CreateRequest(module);
            var data = request.GetAsync<T>(serializerSettings);
			return data;
        } catch(Exception e)
		{
			throw;
		}
    }
    public Task<T> GetAsync<T>(string module, object id)
    {
        try
        {
            var request = CreateRequest(module, id);
            var data = request.GetAsync<T>(serializerSettings);
            return data;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private SLRequest CreateRequest(string module, object? id = null) => id switch
	{
		null => _serviceLayer.Request(module),
		int => _serviceLayer.Request(module, id),
		string => _serviceLayer.Request(module, id),
        _ => throw new ArgumentException("Unknown type of id", nameof(id))
	};

    //    // Performs a POST on /Orders with the provided object as the JSON body, 
    //    // creating a new order and deserializing the created order in a custom model class
    //    var createdOrder = await _serviceLayer.Request("Orders").PostAsync<MyOrderModel>(myNewOrderObject);

    //    // Performs a PATCH on /BusinessPartners('C00001'), updating the CardName of the Business Partner
    //    await _serviceLayer.Request("BusinessPartners", "C00001").PatchAsync(new { CardName = "Updated BP name" });
}


public class SapServiceLayerLogin
{
	public string Uri { get; set; } = string.Empty;
	public string CompanyDB { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}