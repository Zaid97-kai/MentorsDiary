using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MentorsDiary.Web.Components.Groups;

/// <summary>
/// Class GroupList.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupList
{
    /// <summary>
    /// Gets or sets the group service.
    /// </summary>
    /// <value>The group service.</value>
    [Inject]
    private GroupService GroupService { get; set; } = null!;

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
    /// Gets or sets the groups.
    /// </summary>
    /// <value>The groups.</value>
    private List<Group>? Groups { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is create loading.
    /// </summary>
    /// <value><c>true</c> if this instance is create loading; otherwise, <c>false</c>.</value>
    private bool IsCreateLoading { get; set; }

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => "group";

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
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

        switch (CurrentUser.Role)
        {
            case EnumRoles.Administrator:
                Groups = (await GroupService.GetAllAsync() ?? Array.Empty<Group>()).ToList();
                break;
            case EnumRoles.DeputyDirector:
            {
                var result = await GroupService.GetAllByFilterAsync(
                    new FilterParams
                    {
                        ColumnName = "DivisionId",
                        FilterOption = EnumFilterOptions.Contains,
                        FilterValue = CurrentUser.DivisionId.ToString()!
                    });
                Groups = JsonConvert.DeserializeObject<List<Group>>(await result.Content.ReadAsStringAsync());
                break;
            }
        }

        _isLoading = false;
        StateHasChanged();
    }

    /// <summary>
    /// Create group as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CreateGroupAsync()
    {
        IsCreateLoading = true;
        StateHasChanged();

        var response = new HttpResponseMessage();

        switch (CurrentUser.Role)
        {
            case EnumRoles.Administrator:
                response = await GroupService.CreateAsync(new Group
                {
                    DateCreated = DateTime.Now,
                    UserCreated = CurrentUser.Name
                });
                break;
            case EnumRoles.DeputyDirector:
                response = await GroupService.CreateAsync(new Group
                {
                    DivisionId = CurrentUser.DivisionId,
                    DateCreated = DateTime.Now,
                    UserCreated = CurrentUser.Name
                });
                break;
        }

        if (response.IsSuccessStatusCode)
            NavigationManager.NavigateTo($"{NavigateToUri}/{JsonConvert.DeserializeObject<Group>(await response.Content.ReadAsStringAsync())!.Id}");
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
        if (division != null)
        {
            _isLoading = true;
            StateHasChanged();

            if (division.Name != null)
            {
                var result = await GroupService.GetAllByFilterAsync(
                    new FilterParams()
                    {
                        ColumnName = "DivisionId",
                        FilterOption = EnumFilterOptions.Contains,
                        FilterValue = division.Id.ToString()
                    });
                Groups = JsonConvert.DeserializeObject<List<Group>>(await result.Content.ReadAsStringAsync());
            }

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Searches the list.
    /// </summary>
    /// <param name="query">The query.</param>
    private async Task SearchList(string query)
    {
        if (query != string.Empty)
        {
            _isLoading = true;
            StateHasChanged();

            Groups = (await GroupService.GetAllByFilterAsync(query) ?? Array.Empty<Group>()).ToList();

            _isLoading = false;
            StateHasChanged();
        }
        else
            await GetListAsync();
    }

    /// <summary>
    /// Remove as an asynchronous operation.
    /// </summary>
    /// <param name="group">The group.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task RemoveAsync(Group group)
    {
        var response = await GroupService.DeleteAsync(group.Id);
        if (response.IsSuccessStatusCode)
            await MessageService.Success($"Группа {group.Name} успешно удалена.");
        else
            await MessageService.Error(response.ReasonPhrase);

        await GetListAsync();

        StateHasChanged();
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="group">The group.</param>
    private void UpdateAsync(Group group)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}/{group.Id}");
    }

    /// <summary>
    /// Shows the group page asynchronous.
    /// </summary>
    /// <param name="group">The group.</param>
    private void ShowGroupPageAsync(Group group)
    {
        NavigationManager.NavigateTo($"{NavigateToUri}-page/{group.Id}");
    }
}