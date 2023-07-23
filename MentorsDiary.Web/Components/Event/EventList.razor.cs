using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Divisions.Domains;
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
    /// Gets or sets a value indicating whether this instance is create loading.
    /// </summary>
    /// <value><c>true</c> if this instance is create loading; otherwise, <c>false</c>.</value>
    private bool IsCreateLoading { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => "event";

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
        Events = (await EventService.GetAllAsync() ?? Array.Empty<Application.Entities.Events.Domains.Event>()).ToList();
    }

    /// <summary>
    /// Create deputy director as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateDeputyDirectorAsync()
    {
        IsCreateLoading = true;
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

        IsCreateLoading = false;
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
    
    private void UpdateAsync(Application.Entities.Events.Domains.Event @event)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{@event.Id}");
    }

    private Task UpdateList()
    {
        throw new NotImplementedException();
    }

    private Task UpdateListAfterClearDatePicker()
    {
        throw new NotImplementedException();
    }
}