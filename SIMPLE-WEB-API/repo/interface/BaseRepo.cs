namespace SIMPLE_WEB_API.repo
{
    public interface BaseRepo<T>
    {
        public Task GetAllAsync();
        public Task DeleteAsync(Guid id);
        public Task CreateAsync(T items);
        public Task UpdateAsync(T items);
        public Task<T?> GetById(Guid id);

    }
}
