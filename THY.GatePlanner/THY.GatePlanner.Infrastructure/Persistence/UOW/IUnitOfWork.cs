using THY.GatePlanner.Infrastructure.Persistence.Repositories;
using THY.GatePlanner.Model.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace THY.GatePlanner.Infrastructure.Persistence.UOW;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : Base;
    Task<IDbContextTransaction> BeginNewTransaction();
    Task<bool> RollBackTransaction(IDbContextTransaction? transaction);
    Task TransactionCommit(IDbContextTransaction? transaction);
    Task<int> SaveChangesAsync();
}