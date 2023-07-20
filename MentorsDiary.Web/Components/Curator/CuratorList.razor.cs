using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Components.Curator;

/// <summary>
/// Class CuratorList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class CuratorList
{
    /// <summary>
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private UserService UserService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the curator service.
    /// </summary>
    /// <value>The curator service.</value>
    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

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
    /// The is loading
    /// </summary>
    private bool _isLoading;

    /// <summary>
    /// The users
    /// </summary>
    /// <value>The users.</value>
    private List<User>? Users { get; set; }

    /// <summary>
    /// Gets or sets the curators.
    /// </summary>
    /// <value>The curators.</value>
    private List<Application.Entities.Curators.Domains.Curator>? Curators { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is create loading.
    /// </summary>
    /// <value><c>true</c> if this instance is create loading; otherwise, <c>false</c>.</value>
    private bool IsCreateLoading { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => "curator";

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
        Users = (await UserService.GetAllAsync() ?? Array.Empty<User>()).Where(u => u.Role == EnumRoles.Curator)
            .ToList();

        Curators = (await CuratorService.GetAllAsync() ?? Array.Empty<Application.Entities.Curators.Domains.Curator>()).ToList();
    }

    /// <summary>
    /// Create curator as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateCuratorAsync()
    {
        IsCreateLoading = true;
        StateHasChanged();

        var responseUserCreate = await UserService.CreateAsync(new User());
        var deserializeObject = JsonConvert.DeserializeObject<User>(await responseUserCreate.Content.ReadAsStringAsync());

        if (deserializeObject != null)
        {
            var responseCuratorCreate =
                await CuratorService.CreateAsync(new Application.Entities.Curators.Domains.Curator() { UserId = deserializeObject.Id });

            if (responseUserCreate.IsSuccessStatusCode && responseCuratorCreate.IsSuccessStatusCode)
                NavigationManager.NavigateTo(
                    $"{NavigateToUri}/{JsonConvert.DeserializeObject<Application.Entities.Curators.Domains.Curator>(await responseCuratorCreate.Content.ReadAsStringAsync())!.Id}");
            else
                await MessageService.Error($"{responseUserCreate.ReasonPhrase}\n{responseCuratorCreate.ReasonPhrase}");
        }

        IsCreateLoading = false;
    }

    /// <summary>
    /// Updates the list.
    /// </summary>
    /// <param name="division">The division.</param>
    private async Task UpdateList(Division? division)
    {
        StateHasChanged();
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

            Curators = (await CuratorService.GetAllByFilterAsync(query!) ?? Array.Empty<Application.Entities.Curators.Domains.Curator>()).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="curator">The curator.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(Application.Entities.Curators.Domains.Curator curator)
    {
        var responseDeleteUser = await UserService.DeleteAsync(curator.UserId);
        var responseDeleteCurator = await CuratorService.DeleteAsync(curator.Id);

        if (responseDeleteUser.IsSuccessStatusCode && responseDeleteCurator.IsSuccessStatusCode)
            await MessageService.Success($"Пользователь {curator.Name} успешно удален.");
        else
            await MessageService.Error($"{responseDeleteUser.ReasonPhrase}\n{responseDeleteCurator.ReasonPhrase}");

        await GetListAsync();

        StateHasChanged();
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="curator">The curator.</param>
    private void UpdateAsync(Application.Entities.Curators.Domains.Curator curator)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{curator.Id}");
    }
}