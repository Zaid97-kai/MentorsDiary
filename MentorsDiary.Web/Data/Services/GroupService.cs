﻿using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class GroupService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
public class GroupService: BaseService<Group>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupService"/> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    public GroupService(IHttpClientFactory clientFactory) : base(clientFactory)
    {

    }
}