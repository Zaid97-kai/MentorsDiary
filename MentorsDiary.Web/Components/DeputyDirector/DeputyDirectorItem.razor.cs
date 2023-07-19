using AntDesign;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.DeputyDirector;

/// <summary>
/// Class DeputyDirectorItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class DeputyDirectorItem
{
    /// <summary>
    /// Gets or sets the deputy director identifier.
    /// </summary>
    /// <value>The deputy director identifier.</value>
    [Parameter]
    public int DeputyDirectorId { get; set; }

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
    public UserService UserService { get; set; } = null!;

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
    private User? _deputyDirector = new();

    /// <summary>
    /// The divisions
    /// </summary>
    /// <value>The divisions.</value>
    private List<Division>? Divisions { get; set; } = new();

    /// <summary>
    /// Gets or sets the selected division.
    /// </summary>
    /// <value>The selected division.</value>
    private string? SelectedDivisionItem { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the selected division.
    /// </summary>
    /// <value>The selected division.</value>
    private Division? SelectedDivision { get; set; } = new();
    
    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private string NavigateToUri => "deputydirector";

    #endregion

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();

        _deputyDirector = await UserService.GetIdAsync(DeputyDirectorId);
    }

    /// <summary>
    /// Save as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SaveAsync()
    {
        if (_deputyDirector != null)
        {
            _deputyDirector.Role = EnumRoles.DeputyDirector;
            _deputyDirector.Division = null;

            var response = await UserService.UpdateAsync(_deputyDirector);

            if (response.IsSuccessStatusCode)
                await MessageService.Success($"Пользователь {_deputyDirector.Name} успешно добавлен");
            else
                await MessageService.Error(response.ReasonPhrase);
        }

        NavigationManager.NavigateTo("/deputydirector");
    }

    /// <summary>
    /// Called when [selected item changed handler].
    /// </summary>
    /// <param name="value">The value.</param>
    private void OnSelectedItemChangedHandler(Division value)
    {
        SelectedDivision = value;
        StateHasChanged();
    }
}