using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Components.Event;

/// <summary>
/// Class EventList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class EventList
{
    /// <summary>
    /// Gets or sets the event service.
    /// </summary>
    /// <value>The event service.</value>
    [Inject]
    private EventService EventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    private List<Application.Entities.Events.Domains.Event>? Events { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => "event";

    /// <summary>
    /// Gets the selected date time.
    /// </summary>
    /// <value>The selected date time.</value>
    public DateTime?[]? SelectedDateTime { get; private set; }

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetListAsync();
        StateHasChanged();
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

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Create event as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateEventAsync()
    {
        _isLoading = true;
        StateHasChanged();
        var response = await EventService.CreateAsync(new Application.Entities.Events.Domains.Event()
        {
            DateCreated = DateTime.Now,
            UserCreated = CurrentUser.Name
        });

        if (response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"{NavigateToUri}/{JsonConvert.DeserializeObject<Application.Entities.Events.Domains.Event>(await response.Content.ReadAsStringAsync())!.Id}");
        else
            await MessageService.Error(response.ReasonPhrase);

        _isLoading = false;
    }

    /// <summary>
    /// Searches the list.
    /// </summary>
    /// <param name="query">The query.</param>
    private async Task SearchList(string? query)
    {
        if (query != string.Empty)
        {
            _isLoading = true;
            StateHasChanged();

            Events = (await EventService.GetAllByFilterAsync(query!) ?? Array.Empty<Application.Entities.Events.Domains.Event>()).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(Application.Entities.Events.Domains.Event @event)
    {
        var response = await EventService.DeleteAsync(@event.Id);
        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Событие {@event.Name} успешно удалено.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();

        StateHasChanged();
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="event">The event.</param>
    private void UpdateAsync(IHaveId @event)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{@event.Id}");
    }

    /// <summary>
    /// Updates the list.
    /// </summary>
    /// <param name="dateTimeRange">The <see cref="DateRangeChangedEventArgs" /> instance containing the event data.</param>
    private async Task UpdateList(DateRangeChangedEventArgs? dateTimeRange)
    {
        SelectedDateTime = dateTimeRange?.Dates!;

        await GetListAsync();
        if (SelectedDateTime[0] != null && SelectedDateTime[1] != null)
        {
            Events = Events?.Where(d => d.DateEvent > SelectedDateTime[0] && d.DateEvent < SelectedDateTime[1]).ToList();
        }

        StateHasChanged();
    }

    /// <summary>
    /// Updates the list after clear date picker.
    /// </summary>
    private async Task UpdateListAfterClearDatePicker()
    {
        await GetListAsync();
    }
}