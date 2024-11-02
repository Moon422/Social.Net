using Microsoft.EntityFrameworkCore;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Domains.Common;

namespace Social.Net.Data;

public class Repository<TEntity>(SocialDbContext dbContext, ITransactionManager transactionManager) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    public IQueryable<TEntity> QueryBuilder => dbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        
        return await dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IList<TEntity>> GetAllAsync(bool orderByIdDesc = false)
    {
        if (orderByIdDesc)
        {
            return await dbContext.Set<TEntity>()
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }
        
        return await dbContext.Set<TEntity>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public Task InsertAsync(TEntity entity, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is ICreationAuditable creationAuditable)
        {
            creationAuditable.CreatedOn = DateTime.UtcNow;
        }

        if (entity is IModificationAuditable modificationAuditable)
        {
            modificationAuditable.ModifiedOn = DateTime.UtcNow;
        }
        
        return deferInsert ? InsertCall() : transactionManager.RunTransactionAsync(InsertCall);

        async Task InsertCall() => await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is IModificationAuditable modificationAuditable)
        {
            modificationAuditable.ModifiedOn = DateTime.UtcNow;
        }

        return deferUpdate ? UpdateCall() : transactionManager.RunTransactionAsync(UpdateCall);
        
        Task UpdateCall() => Task.FromResult(() => dbContext.Set<TEntity>().Update(entity));
    }

    public Task DeleteAsync(TEntity entity, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is not ISoftDeleted softDeleted)
        {
            return deferDelete ? DeleteCall() : transactionManager.RunTransactionAsync(DeleteCall);
        }
        
        softDeleted.IsDeleted = true;
        softDeleted.DeletedOn = DateTime.UtcNow;
        return UpdateAsync(entity, deferDelete);

        Task DeleteCall() => Task.FromResult(dbContext.Set<TEntity>().Remove(entity));
    }
}