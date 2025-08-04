namespace TodoApi.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ITodoRepository Todo { get; }
        Task SaveAsync();
    }
}
