using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Groups;

/// <summary>
/// Class GroupItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class GroupItem
{
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
    /// Gets or sets the user service.
    /// </summary>
    /// <value>The user service.</value>
    [Inject]
    private GroupService GroupService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the curator service.
    /// </summary>
    /// <value>The curator service.</value>
    [Inject]
    private CuratorService CuratorService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the division service.
    /// </summary>
    /// <value>The division service.</value>
    [Inject]
    private DivisionService DivisionService { get; set; } = null!;

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

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    /// <summary>
    /// The deputy director
    /// </summary>
    private Group? _group = new();

    /// <summary>
    /// The divisions
    /// </summary>
    /// <value>The divisions.</value>
    private List<Division>? Divisions { get; set; } = new();

    /// <summary>
    /// Gets or sets the curators.
    /// </summary>
    /// <value>The curators.</value>
    private List<Application.Entities.Curators.Domains.Curator>? Curators { get; set; } = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "group";

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
                Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Application.Entities.Curators.Domains.Curator>()).ToList();
                break;
            case EnumRoles.DeputyDirector:
                Curators = (await CuratorService.GetAllAsync() ??
                            Array.Empty<Application.Entities.Curators.Domains.Curator>())
                    .Where(c => c.User?.DivisionId == CurrentUser.DivisionId).ToList();
                break;
        }

        _isLoading = true;
        StateHasChanged();

        _group = await GroupService.GetIdAsync(GroupId);
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        _isLoading = true;
        StateHasChanged();

        if (_group != null)
        {
            _group.Division = null;
            _group.Curator = null;

            if (CurrentUser.Role == EnumRoles.DeputyDirector)
                _group.DivisionId = CurrentUser.DivisionId;

            var response = await GroupService.UpdateAsync(_group);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Группа {_group.Name} успешно добавлена.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo("/group");
    }
}