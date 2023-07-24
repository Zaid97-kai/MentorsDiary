using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.GroupEvents.Repositories;

/// <summary>
/// Class GroupEventRepository.
/// Implements the <see cref="BaseRepository{GroupEvent}" />
/// Implements the <see cref="IGroupEventRepository" />
/// </summary>
/// <seealso cref="BaseRepository{GroupEvent}" />
/// <seealso cref="IGroupEventRepository" />
public class GroupEventRepository : BaseRepository<GroupEvent>, IGroupEventRepository
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly IMentorsDiaryContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventRepository" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public GroupEventRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudent">The group event student.</param>
    public async Task AddStudentsInGroupEvent(GroupEventStudent groupEventStudent)
    {
        var groupEvent = await ((DbContext)_context).Set<GroupEvent>().FirstOrDefaultAsync(g => g.Id == groupEventStudent.Id);

        if (groupEvent != null)
        {
            if (groupEvent.Students != null)
            {
                //TODO: FIX EXCEPT
                groupEvent.Students = groupEvent.Students.Except(groupEventStudent.Students).ToList();
            }

            ((DbContext)_context).Entry(groupEvent).CurrentValues.SetValues(groupEvent);
            ((DbContext)_context).Entry(groupEvent).State = EntityState.Modified;
        }

        await Context.SaveChangesAsync()!;
    }
}