﻿using AntDesign;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

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

    private Application.Entities.Events.Domains.Event? Clone { get; set; } = new();

    private string? _avatar;

    private string? _newAvatar;

    private IBrowserFile? _resizedImage;

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await GetItemAsync();
        await UploadAvatarPath();
    }

    private async Task UploadAvatarPath()
    {
        if (_event!.ImagePath != null)
        {
            var result = await EventService?.GetAvatarAsync(_event!.ImagePath)!;
            if (result != null)
                _avatar = result.RequestMessage?.RequestUri?.ToString();
            else
                await MessageService?.Error("Ошибка фотографии")!;
        }
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
            await UploadAvatar();
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
    private async Task UploadAvatar()
    {
        using var content = new MultipartFormDataContent();
        var fileName = Path.GetRandomFileName();

        content.Add(
            content: new StreamContent(_resizedImage?.OpenReadStream() ?? Stream.Null),
            name: "\"files\"",
            fileName: fileName);

        var response = await EventService?.UploadAvatarAsync(content)!;

        if (response.IsSuccessStatusCode)
        {
            _event!.ImagePath = fileName;
            Clone!.ImagePath = fileName;

            await MessageService.Success("Upload completed successfully.");
            var result = await EventService.GetAvatarAsync(_event!.ImagePath);
            _avatar = result.RequestMessage?.RequestUri?.ToString();
        }
        else
            await MessageService.Error("Upload failed.");
    }

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        var imageFile = e.File;
        if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
        {
            await MessageService.Error("You can only upload JPG/PNG file!");
        }
        else
        {
            _resizedImage = await imageFile.RequestImageFileAsync("image/png", 500, 500);

            var ms = new MemoryStream();
            await _resizedImage.OpenReadStream().CopyToAsync(ms);
            var bytes = ms.ToArray();

            var b64 = Convert.ToBase64String(bytes);

            _newAvatar = "data:image/png;base64," + b64;
            _avatar = _newAvatar;
        }

        StateHasChanged();
    }
}