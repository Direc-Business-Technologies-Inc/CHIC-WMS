namespace Application.Libraries.Repositories
{
    public interface IServiceLayerDataAccess
    {
        Task<HttpResponseMessage[]> BatchAsync(SLBatchRequest[] sLBatchRequests);
        void Logout();
        Task<U> PatchAsync<U>(string Module, dynamic Id, U data);
        Task<string> PatchStringAsync(string Module, dynamic Id, string data);

		Task<T> PostAsync<T, U>(string Module, U data);
		Task<T> GetAsync<T>(string module);
		Task<T> GetAsync<T>(string module, object id);
    }
}