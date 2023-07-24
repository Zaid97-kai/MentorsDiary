using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.GroupEvents.Domains;

namespace MentorsDiary.Application.Entities.GroupEvents.Interfaces;

/// <summary>
/// Interface IGroupEventRepository
/// Extends the <see cref="IBaseRepository{GroupEvent}" />
/// </summary>
/// <seealso cref="IBaseRepository{GroupEvent}" />
public interface IGroupEventRepository : IBaseRepository<GroupEvent>
{
    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="students">The students.</param>
    /// <returns>Task.</returns>
    Task AddStudentsInGroupEvent(GroupEventStudent students);
}