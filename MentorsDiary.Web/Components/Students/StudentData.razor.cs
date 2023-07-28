using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Components.Students;

/// <summary>
/// Class StudentData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class StudentData
{
    /// <summary>
    /// Gets or sets the student.
    /// </summary>
    /// <value>The student.</value>
    [Parameter]
    public Student? Student { get; set; }

    /// <summary>
    /// Gets or sets the student changed.
    /// </summary>
    /// <value>The student changed.</value>
    [Parameter]
    public EventCallback<Student>? StudentChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="StudentData" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [Parameter]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the change visible.
    /// </summary>
    /// <value>The change visible.</value>
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets the parent student service.
    /// </summary>
    /// <value>The parent student service.</value>
    [Inject]
    private ParentStudentService ParentStudentService { get; set; } = null!;

    /// <summary>
    /// The parents
    /// </summary>
    private List<Parent?> _parents = new();

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetItemAsync();
    }

    /// <summary>
    /// Get item as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetItemAsync()
    {
        _isLoading = true;
        StateHasChanged();

        _parents = JsonConvert.DeserializeObject<List<ParentStudent>>(await (await ParentStudentService.GetAllByFilterAsync(
            new FilterParams()
            {
                ColumnName = "StudentId",
                FilterOption = EnumFilterOptions.Contains,
                FilterValue = Student?.Id.ToString()!
            })).Content.ReadAsStringAsync())!.Select(s => s.Parent).ToList();

        _isLoading = false;
        StateHasChanged();
    }
    
    /// <summary>
    /// Closes the student page.
    /// </summary>
    private void CloseStudentPage()
    {
        Visible = !Visible;
    }
}