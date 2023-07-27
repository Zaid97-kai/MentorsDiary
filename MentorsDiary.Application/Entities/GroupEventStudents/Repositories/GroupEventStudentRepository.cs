﻿using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Interfaces;
using MentorsDiary.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.GroupEventStudents.Repositories;

/// <summary>
/// Class GroupEventStudentRepository.
/// Implements the <see cref="BaseRepository{GroupEventStudent}" />
/// Implements the <see cref="IGroupEventStudentRepository" />
/// </summary>
/// <seealso cref="BaseRepository{GroupEventStudent}" />
/// <seealso cref="IGroupEventStudentRepository" />
public class GroupEventStudentRepository : BaseRepository<GroupEventStudent>, IGroupEventStudentRepository
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly IMentorsDiaryContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventStudentRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public GroupEventStudentRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudents">The group event students.</param>
    public async Task AddStudentsInGroupEvent(List<GroupEventStudent> groupEventStudents)
    {
        var eventStudents = _context.GroupEventStudents.Where(g => g.GroupEventId == groupEventStudents.First().GroupEventId);
        if(eventStudents != null)
        {
            _context.GroupEventStudents.RemoveRange(eventStudents);
            await Context.SaveChangesAsync();
        }
        
        await _context.GroupEventStudents.AddRangeAsync(groupEventStudents);
        await Context.SaveChangesAsync();
    }
}