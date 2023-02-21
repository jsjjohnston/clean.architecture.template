namespace Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChanagesAsync(CancellationToken cancellationToken = default);
}
