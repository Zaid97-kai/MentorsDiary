using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Persistence.DataSeeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MentorsDiary.Persistence;

/// <summary>
/// Class MentorsDiaryDbContext.
/// Implements the <see cref="DbContext" />
/// Implements the <see cref="IMentorsDiaryContext" />
/// </summary>
/// <seealso cref="DbContext" />
/// <seealso cref="IMentorsDiaryContext" />
public class MentorsDiaryDbContext : DbContext, IMentorsDiaryContext
{
    /// <summary>
    /// The service provider
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="MentorsDiaryDbContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="serviceProvider">The service provider.</param>
    public MentorsDiaryDbContext(DbContextOptions<MentorsDiaryDbContext> options, IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks><para>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run. However, it will still run when creating a compiled model.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    /// examples.
    /// </para></remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder = DataSeederUser.SeedData(modelBuilder);
        modelBuilder = DataSeederDivision.SeedData(modelBuilder);
    }

    /// <summary>
    /// Override this method to configure the database (and other options) to be used for this context.
    /// This method is called for each instance of the context that is created.
    /// The base implementation does nothing.
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
    /// typically define extension methods on this object that allow you to configure the context.</param>
    /// <remarks><para>
    /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
    /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
    /// the options have already been set, and skip some or all of the logic in
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information and examples.
    /// </para></remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder
            .ConfigureWarnings(x => x.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
    }

    #region ENTITIES

    /// <summary>
    /// Gets or sets the curators.
    /// </summary>
    /// <value>The curators.</value>
    public DbSet<Curator> Curators { get; set; }

    /// <summary>
    /// Gets or sets the divisions.
    /// </summary>
    /// <value>The divisions.</value>
    public DbSet<Division> Divisions { get; set; }

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    public DbSet<Event> Events { get; set; }

    /// <summary>
    /// Gets or sets the group events.
    /// </summary>
    /// <value>The group events.</value>
    public DbSet<GroupEvent> GroupEvents { get; set; }

    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>The groups.</value>
    public DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Gets or sets the parents.
    /// </summary>
    /// <value>The parents.</value>
    public DbSet<Parent> Parents { get; set; }

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    /// <value>The students.</value>
    public DbSet<Student> Students { get; set; }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>The users.</value>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the group event student.
    /// </summary>
    /// <value>The group event student.</value>
    public DbSet<GroupEventStudent> GroupEventStudents { get; set; }

    #endregion
}