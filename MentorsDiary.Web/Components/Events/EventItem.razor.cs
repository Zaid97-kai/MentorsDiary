using AntDesign;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Events;

/// <summary>
/// Class EventItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class EventItem
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the event identifier.
    /// </summary>
    /// <value>The event identifier.</value>
    [Parameter]
    public int EventId { get; set; }

    #endregion

    #region INJECTIONS

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

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
    private MessageService MessageService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The event
    /// </summary>
    private Event? _event = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "event";

    /// <summary>
    /// The is loading
    /// </summary>
    private bool _isLoading;

    #endregion

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

        _event = await EventService.GetIdAsync(EventId);

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_event != null)
        {
            var response = await EventService.UpdateAsync(_event);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Событие {_event.Name} успешно добавлено.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo(NavigateToUri);
    }
}