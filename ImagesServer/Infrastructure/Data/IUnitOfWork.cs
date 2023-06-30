using Domain.Repository;

namespace Infrastructure.Data;

public interface IUnitOfWork
{
    public IImageRepository ImageRepository { get; }
    Task CommitAsync();
}