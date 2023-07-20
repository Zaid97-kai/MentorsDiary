using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MentorsDiary.Application.Entities.Bases.Repositories;

/// <summary>
/// Class BaseRepository.
/// Implements the <see cref="IBaseRepository{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="IBaseRepository{T}" />
public abstract class BaseRepository<T> : IBaseRepository<T> 
    where T : class, IHaveId, IHaveName
{
    /// <summary>
    /// The context
    /// </summary>
    protected readonly DbContext Context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{T}" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    protected BaseRepository(DbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task&lt;System.Nullable&lt;T&gt;&gt;.</returns>
    public async Task<T?> GetById(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Gets all.
    /// </summary>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;T&gt;&gt;&gt;.</returns>
    public async Task<IEnumerable<T>?> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Gets all by filter.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;T&gt;&gt;&gt;.</returns>
    public async Task<IEnumerable<T>?> GetAllByFilter(string query)
    {
        return await Context.Set<T>().Where(t => t.Name == query).ToListAsync();
    }

    /// <summary>
    /// Finds the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;T&gt;&gt;&gt;.</returns>
    public Task<IEnumerable<T>?> Find(Expression<Func<T, bool>> expression)
    {
        return Task.FromResult<IEnumerable<T>?>(Context?.Set<T>().Where(expression));
    }

    /// <summary>
    /// Adds the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    public async Task Add(T entity)
    {
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync()!;
    }

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    public async Task Update(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync()!;
    }

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    public async Task Remove(T entity)
    {
        Context?.Set<T>().Remove(entity);
        await Context.SaveChangesAsync()!;
    }
}