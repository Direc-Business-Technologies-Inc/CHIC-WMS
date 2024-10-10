namespace Application.Services.Repositories
{
    public interface IGenericService<T> : IGenericService<T, int> where T : class
    {
    }

    public interface IGenericService<T, U> where T : class
    {
        T Get(U id);
        List<T> GetAll();
        T Create(T data);
    }

    public interface IGenericAsyncService<T> : IGenericAsyncService<T, int> where T : class
    {
    }
    public interface IGenericAsyncService<T, U> where T : class
    {
        Task<T> GetAsync(U id);
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T data);
    }

    public abstract class GenericService<T> : GenericService<T, int> where T : class { }

    public abstract class GenericService<T, U> : IGenericService<T, U>, IGenericAsyncService<T, U> where T : class
    {
        public abstract T Get(U id);
        public abstract List<T> GetAll();
        public abstract T Create(T data);
        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<T> GetAsync(U id);
        public abstract Task<T> CreateAsync(T data);
    }

}
