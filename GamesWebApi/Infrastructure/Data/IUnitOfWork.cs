namespace Infrastructure.Data;

public interface IUnitOfWork
{
    void Commit();
}