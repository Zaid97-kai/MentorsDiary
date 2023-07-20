using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Components.DeputyDirector;

/// <summary>
/// Class DeputyDirectorList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class DeputyDirectorList
{
    /// <summary>
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private UserService UserService { get; set; } = null!;

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
    private IMessageService MessageService { get; set; }

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
    /// Gets or sets a value indicating whether this instance is create loading.
    /// </summary>
    /// <value><c>true</c> if this instance is create loading; otherwise, <c>false</c>.</value>
    private bool IsCreateLoading { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => "deputydirector";

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
        Users = (await UserService.GetAllAsync() ?? Array.Empty<User>()).Where(u => u.Role == EnumRoles.DeputyDirector)
            .ToList();
    }

    /// <summary>
    /// Create deputy director as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateDeputyDirectorAsync()
    {
        IsCreateLoading = true;
        StateHasChanged();
        var response = await UserService.CreateAsync(new User());
        
        if(response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"{NavigateToUri}/{JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync())!.Id}");
        else
            await MessageService.Error(response.ReasonPhrase);

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
                
            Users = (await UserService.GetAllByFilterAsync(query!) ?? Array.Empty<User>()).Where(u => u.Role == EnumRoles.DeputyDirector).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(User user)
    {
        var response = await UserService.DeleteAsync(user.Id);
        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Пользователь {user.Name} успешно удален.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();

        StateHasChanged();
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="user">The user.</param>
    private void UpdateAsync(User user)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{user.Id}");
    }
}