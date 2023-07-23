using AntDesign;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.GroupEvents;

/// <summary>
/// Class GroupEventItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupEventItem
{
    /// <summary>
    /// Gets or sets the group event identifier.
    /// </summary>
    /// <value>The group event identifier.</value>
    [Parameter]
    public int GroupEventId { get; set; }

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
    /// Gets or sets the group event service.
    /// </summary>
    /// <value>The group event service.</value>
    [Inject]
    public GroupEventService GroupEventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the event service.
    /// </summary>
    /// <value>The event service.</value>
    [Inject] 
    private EventService EventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the student service.
    /// </summary>
    /// <value>The student service.</value>
    [Inject] 
    private StudentService StudentService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The student
    /// </summary>
    private GroupEvent? _groupEvent = new();

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    private List<Application.Entities.Events.Domains.Event> Events { get; set; } = new();

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    /// <value>The students.</value>
    private List<Student> Students { get; set; } = new();

    /// <summary>
    /// Gets or sets the transfer students.
    /// </summary>
    /// <value>The transfer students.</value>
    private List<TransferItem> TransferStudents { get; set; } = new();

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// The titles
    /// </summary>
    private readonly string[] _titles = { "Все", "Участники" };

    /// <summary>
    /// The target keys
    /// </summary>
    private IEnumerable<string?> _targetKeys;

    /// <summary>
    /// The selected keys
    /// </summary>
    private IEnumerable<string> _selectedKeys = new List<string>();

    /// <summary>
    /// Gets or sets the selected students.
    /// </summary>
    /// <value>The selected students.</value>
    private List<Student?> SelectedStudents { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => $"/group-page/{GroupId}";

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        _groupEvent = await GroupEventService.GetIdAsync(GroupEventId);

        await GetListAsync();
    }

    /// <summary>
    /// Get list as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task GetListAsync()
    {
        _isLoading = true;
        StateHasChanged();
        
        Events = (await EventService.GetAllAsync() ?? Array.Empty<Application.Entities.Events.Domains.Event>()).ToList();
        Students = (await StudentService.GetAllAsync() ?? Array.Empty<Student>()).Where(s => s.GroupId == GroupId).ToList();

        TransferStudents = Students.Select(s => new TransferItem { Key = s.Id.ToString(), Title = s.Name, Description = s.Name }).ToList();
        _targetKeys = Students.Select(s => s.Name);

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        if (_groupEvent != null)
        {
            _groupEvent.Event = null;
            _groupEvent.GroupId = GroupId;

            var response = await GroupEventService.UpdateAsync(_groupEvent);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Событие {_groupEvent.Name} успешно добавлено");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        NavigationManager.NavigateTo(NavigateToUri);
    }

    /// <summary>
    /// Called when [change].
    /// </summary>
    /// <param name="e">The e.</param>
    private async Task OnChange(TransferChangeArgs e)
    {
        SelectedStudents = new List<Student>()!;

        foreach (var key in e.MoveKeys)
        {
            SelectedStudents.Add(await StudentService.GetIdAsync(Convert.ToInt32(key)));
        }
    }
}