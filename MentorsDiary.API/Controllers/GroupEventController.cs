using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class GroupEventController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent, MentorsDiary.Application.Entities.GroupEvents.Interfaces.IGroupEventRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent, MentorsDiary.Application.Entities.GroupEvents.Interfaces.IGroupEventRepository}" />
public class GroupEventController : BaseController<GroupEvent, IGroupEventRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventController"/> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    public GroupEventController(IGroupEventRepository repository) : base(repository)
    {

    }
}