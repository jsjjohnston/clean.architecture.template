namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChanagesAsync(CancellationToken cancellationToken = default);
}
