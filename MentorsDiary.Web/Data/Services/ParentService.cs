﻿using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class ParentService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
public class ParentService: BaseService<Parent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentService"/> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    public ParentService(IHttpClientFactory clientFactory) : base(clientFactory)
    {

    }
}