using Social.Net.Core.Domains.Common;

namespace Social.Net.Data;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> QueryBuilder { get; }

    /// <summary>
    /// Read one entity from db with primary key
    /// </summary>
    /// <param name="id">Primary key</param>
    /// <returns>Entity with primary key of id</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Query all entities from db
    /// </summary>
    /// <returns>List of entities read from db</returns>
    Task<IList<TEntity>> GetAllAsync(bool orderByIdDesc = false);
    
    /// <summary>
    /// Inserts one entity to db
    /// </summary>
    /// <returns>Task</returns>
    Task InsertAsync(TEntity entity, bool deferInsert = false);
    
    /// <summary>
    /// Updates one entity to db
    /// </summary>
    /// <returns>Task</returns>
    Task UpdateAsync(TEntity entity, bool deferUpdate = false);
    
    /// <summary>
    /// Deletes one entity from db
    /// </summary>
    /// <returns>Task</returns>
    Task DeleteAsync(TEntity entity, bool deferDelete = false);
}