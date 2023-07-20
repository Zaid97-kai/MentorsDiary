using AntDesign;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
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
    public GroupService GroupService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the curator service.
    /// </summary>
    /// <value>The curator service.</value>
    [Inject]
    public CuratorService CuratorService { get; set; } = null!;

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
    private MessageService MessageService { get; set; } = null!;

    #endregion

    #region PROPERTIES

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
    private string NavigateToUri => "group";

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
        Curators = (await CuratorService.GetAllAsync() ?? Array.Empty<Application.Entities.Curators.Domains.Curator>()).ToList();

        _group = await GroupService.GetIdAsync(GroupId);
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        if (_group != null)
        {
            _group.Division = null;

            var response = await GroupService.UpdateAsync(_group);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Группа {_group.Name} успешно добавлена.");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        NavigationManager.NavigateTo("/group");
    }
}