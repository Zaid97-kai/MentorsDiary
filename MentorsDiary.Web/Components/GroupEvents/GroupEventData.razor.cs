using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.GroupEvents;

/// <summary>
/// Class GroupEventData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupEventData
{
    [Parameter]
    public required GroupEvent GroupEvent { get; set; }

    [Parameter]
    public bool Visible { get; set; }

    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    [Inject]
    private GroupEventStudentService GroupEventStudentService { get; set; } = null!;

    private List<Student> _students = new();

    private bool _isLoading;

    protected override async Task OnInitializedAsync()
    {
        await GetItemsAsync();
    }

    private async Task GetItemsAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _students = (await GroupEventStudentService.GetAllByFilterAsync(new FilterParams
        {
            ColumnName = "GroupEventId",
            FilterOption = EnumFilterOptions.Contains,
            FilterValue = GroupEvent.Id.ToString()
        }) ?? Array.Empty<GroupEventStudent>()).Select(s => s.Student).ToList()!;

        _isLoading = false;
        StateHasChanged();
    }
}