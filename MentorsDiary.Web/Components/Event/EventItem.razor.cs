using AntDesign;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Event;

/// <summary>
/// Class EventItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class EventItem
{
    /// <summary>
    /// Gets or sets the event identifier.
    /// </summary>
    /// <value>The event identifier.</value>
    [Parameter]
    public int EventId { get; set; }

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
    public EventService EventService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// The event
    /// </summary>
    private Application.Entities.Events.Domains.Event? _event = new();

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    private List<Application.Entities.Events.Domains.Event>? Events { get; set; } = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "event";

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        _event = await EventService.GetIdAsync(EventId);
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        if (_event != null)
        {
            var response = await EventService.UpdateAsync(_event);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Событие {_event.Name} успешно добавлено.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        NavigationManager.NavigateTo(NavigateToUri);
    }
}