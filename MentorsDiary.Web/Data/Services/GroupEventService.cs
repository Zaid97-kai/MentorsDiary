﻿using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class GroupEventService.
/// Implements the <see cref="GroupEvent" />
/// </summary>
/// <seealso cref="GroupEvent" />
public class GroupEventService: BaseService<GroupEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public GroupEventService(HttpClient? httpClient) : base(httpClient)
    {
    }
}