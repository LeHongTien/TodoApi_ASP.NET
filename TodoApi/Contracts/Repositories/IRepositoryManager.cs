namespace TodoApi.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ITodoRepository Todo { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
