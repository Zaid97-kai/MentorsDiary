using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.GroupEvents.Domains;

namespace MentorsDiary.Application.Entities.GroupEvents.Interfaces;

/// <summary>
/// Interface IGroupEventRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent}" />
public interface IGroupEventRepository : IBaseRepository<GroupEvent>
{

}