using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Net;

namespace Application.MauiBlazor.Services
{

    public class RestService
    {
        private const string MEDIA_TYPE = "application/json";

        RestClient _client;
        Uri _uri;
        string _baseAddress;


        public RestService(string baseAddress)
        {

            _baseAddress = baseAddress;

            _uri = new Uri(baseAddress);

            var options = new RestClientOptions(_uri)
            {
            };

            _client = new RestClient(
                options,
                configureSerialization: s => s.UseNewtonsoftJson()
            );
        }

        public Task<T> Get<T>(string path)
        {
            try
            {
                var request = new RestRequest(path);
                var result = _client.ExecuteAsync<T>(request);
                if (result.Result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Result.ErrorMessage);
                }
                //var res = result.Result.Data;
                Task<T> res = Task.FromResult(result.Result.Data);
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public Task<T> Get<T>(string path, object paramId, object paramVal)
        {
            var request = new RestRequest(path, Method.Get);
            request.AddParameter(paramId.ToString(), paramVal.ToString(), ParameterType.GetOrPost);
            var res = _client.GetAsync<T>(request);
            return res;
        }
        public Task<T> Get<T>(string path, string urlSegment, string paramVal)
        {
            var request = new RestRequest(path, Method.Get);
            request.AddUrlSegment(urlSegment, paramVal);
            var res = _client.GetAsync<T>(request);
            return res;
        }

        public Task<T> Post<T>(string path)
        {

            var request = new RestRequest(path);
            var res = _client.PostAsync<T>(request);
            return res;
        }

        public Task<T> Post<T>(string path, object payload)
        {
            var request = new RestRequest(path);            
            request.AddJsonBody(payload, ContentType.Json);
            var res = _client.PostAsync<T>(request);
            return res;
        }

        public Task<RestResponse<T>> ExecutePostAsync<T>(string path, object payload)
        {
            var request = new RestRequest(path);
            request.AddJsonBody(payload, ContentType.Json);
            var res = _client.ExecutePostAsync<T>(request);
            return res;
        }

        public Task<RestResponse> ExecutePostAsync(string path, object payload)
        {
            var request = new RestRequest(path);
            request.AddJsonBody(payload, ContentType.Json);
            var res = _client.ExecutePostAsync(request);
            return res;
        }

    }

    public class RestServiceFactory
    {
        public RestService Create(string baseUri)
        {
            return new RestService(baseUri);
        }
    }
}
