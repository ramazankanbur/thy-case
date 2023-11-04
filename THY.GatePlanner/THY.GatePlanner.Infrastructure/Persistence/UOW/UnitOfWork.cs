using THY.GatePlanner.Infrastructure.Persistence.Repositories;
using THY.GatePlanner.Model.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace THY.GatePlanner.Infrastructure.Persistence.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly GatePlannerContext _context;
    private bool _disposed;

    public UnitOfWork(GatePlannerContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : Base
    {
        return new Repository<T>(_context);
    }

    public async Task<IDbContextTransaction> BeginNewTransaction()
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        return transaction;
    }

    public async Task<bool> RollBackTransaction(IDbContextTransaction? transaction)
    {
        try
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            else
            {
                throw new Exception("UnitOfWork Error: Transaction is null!"); 
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task TransactionCommit(IDbContextTransaction? transaction)
    {
        try
        {
            if (transaction != null)
                await transaction.CommitAsync();
            else
                throw new Exception("UnitOfWork Error: Transaction is null!");  
        }
        catch (Exception ex)
        {
            throw new Exception("Error on save changes ", ex);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            if (_context == null) throw new ArgumentException("Context is null");

            var result = await _context.SaveChangesAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error on save changes ", ex);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }
}