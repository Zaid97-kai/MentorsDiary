﻿using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Bases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorsDiary.API.Controllers.Bases;

/// <summary>
/// Class BaseController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
/// <typeparam name="TRepository">The type of the t repository.</typeparam>
/// <seealso cref="ControllerBase" />
[Route("api/[controller]")]
public class BaseController<TEntity, TRepository> : ControllerBase
    where TEntity : class, IHaveId, IHaveName
    where TRepository : class, IBaseRepository<TEntity>
{
    /// <summary>
    /// The repository
    /// </summary>
    private readonly TRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseController{TEntity, TRepository}" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    public BaseController(TRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Gets all.
    /// </summary>
    /// <returns>System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
    [HttpGet]
    public async Task<IEnumerable<TEntity>?> GetAll()
    {
        var entities = await _repository.GetAll();
        return entities ?? null;
    }

    /// <summary>
    /// Gets all by filter.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
    [HttpGet("Filter/{query}")]
    public async Task<IEnumerable<TEntity>?> GetAllByFilter(string query)
    {
        var entities = await _repository.GetAllByFilter(query);
        return entities ?? null;
    }

    /// <summary>
    /// Gets all by filter.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
    [HttpPost("Filter")]
    public async Task<IEnumerable<TEntity>?> GetAllByFilter([FromBody] FilterParams query)
    {
        if (query.FilterValue == "-1")
            return await _repository.GetAll();

        var entities = await _repository.GetAllByFilter(query);
        return entities ?? null;
    }

    /// <summary>
    /// Gets the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpGet("{id:int}")]
    public async Task<TEntity?> Get(int id)
    {
        var entity = await _repository.GetById(id);
        return entity ?? null;
    }

    /// <summary>
    /// Creates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpPost("Create")]
    public async Task<TEntity?> Create([FromBody] TEntity entity)
    {
        await _repository.Add(entity);
        return entity;
    }

    /// <summary>
    /// Puts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpPut("Update")]
    public async Task<TEntity?> Put([FromBody] TEntity entity)
    {
        await _repository.Update(entity);
        return entity;
    }

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>System.Nullable&lt;TEntity&gt;.</returns>
    [HttpDelete("{id}")]
    public async Task<TEntity?> Delete(int id)
    {
        var entity = (_repository.Find(x => x.Id == id).Result ?? Array.Empty<TEntity>()).FirstOrDefault();

        if (entity != null)
        {
            await _repository.Remove(entity);
            return entity;
        }

        return null;
    }
}