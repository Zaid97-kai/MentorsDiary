using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class GroupEventController.
/// Implements the <see cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent, MentorsDiary.Application.Entities.GroupEvents.Interfaces.IGroupEventRepository}" />
/// </summary>
/// <seealso cref="MentorsDiary.API.Controllers.Bases.BaseController{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent, MentorsDiary.Application.Entities.GroupEvents.Interfaces.IGroupEventRepository}" />
public class GroupEventController : BaseController<GroupEvent, IGroupEventRepository>
{
    /// <summary>
    /// The group event repository
    /// </summary>
    public readonly IGroupEventRepository _groupEventRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    public GroupEventController(IGroupEventRepository repository) : base(repository)
    {
        _groupEventRepository = repository;
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudent">The group event student.</param>
    /// <returns>GroupEventStudent.</returns>
    [HttpPost("AddStudentsInGroupEvent")]
    public async Task<GroupEventStudent> AddStudentsInGroupEvent([FromBody]GroupEventStudent groupEventStudent)
    {
        await _groupEventRepository.AddStudentsInGroupEvent(groupEventStudent);

        return groupEventStudent;
    }
}