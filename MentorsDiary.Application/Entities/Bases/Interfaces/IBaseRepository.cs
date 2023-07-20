using MentorsDiary.Application.Bases.Interfaces.IHaves;
using System.Linq.Expressions;

namespace MentorsDiary.Application.Entities.Bases.Interfaces;

/// <summary>
/// Interface IBaseRepository
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T> 
    where T : class, IHaveId, IHaveName
{
    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task&lt;System.Nullable&lt;T&gt;&gt;.</returns>
    Task<T?> GetById(int id);

    /// <summary>
    /// Gets all.
    /// </summary>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;T&gt;&gt;&gt;.</returns>
    Task<IEnumerable<T>?> GetAll();

    /// <summary>
    /// Gets all by filter.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;T&gt;&gt;&gt;.</returns>
    Task<IEnumerable<T>?> GetAllByFilter(string query);

    /// <summary>
    /// Finds the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;T&gt;&gt;&gt;.</returns>
    Task<IEnumerable<T>?> Find(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Adds the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    Task Add(T entity);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    Task Update(T entity);

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    Task Remove(T entity);
}