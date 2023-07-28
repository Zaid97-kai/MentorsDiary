using AntDesign;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Students;

/// <summary>
/// Class StudentItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class StudentItem
{
    /// <summary>
    /// Gets or sets the student identifier.
    /// </summary>
    /// <value>The student identifier.</value>
    [Parameter]
    public int StudentId { get; set; }

    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    /// <value>The group identifier.</value>
    [Parameter]
    public int GroupId { get; set; }

    #region INJECTIONS

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets the student service.
    /// </summary>
    /// <value>The student service.</value>
    [Inject]
    public StudentService StudentService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private MessageService MessageService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The student
    /// </summary>
    private Student? _student = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => $"/group-page/{GroupId}";

    private bool _isLoading;

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        _student = await StudentService.GetIdAsync(StudentId);
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_student != null)
        {
            _student.GroupId = GroupId;

            var response = await StudentService.UpdateAsync(_student);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Студент {_student.Name} успешно добавлен");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo(NavigateToUri);
    }
}