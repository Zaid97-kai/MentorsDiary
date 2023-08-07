﻿using AntDesign;
using HttpService.Services;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Curators;

/// <summary>
/// Class CuratorItem.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class CuratorItem
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets the curator identifier.
    /// </summary>
    /// <value>The curator identifier.</value>
    [Parameter]
    public int CuratorId { get; set; }

    #endregion

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
    /// The curator
    /// </summary>
    private Application.Entities.Curators.Domains.Curator? _curator = new() { User = new User { Division = new Division() } };

    /// <summary>
    /// The divisions
    /// </summary>
    /// <value>The divisions.</value>
    private List<Division>? Divisions { get; set; } = new();

    /// <summary>
    /// Gets or sets the selected division.
    /// </summary>
    /// <value>The selected division.</value>
    private Division? SelectedDivision { get; set; } = new();

    /// <summary>
    /// Gets the navigate to URI.
    /// </summary>
    /// <value>The navigate to URI.</value>
    private static string NavigateToUri => "curator";

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

        Divisions = (await DivisionService.GetAllAsync() ?? Array.Empty<Division>()).ToList();
        _curator = await CuratorService.GetIdAsync(CuratorId);

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

        if (_curator != null)
        {
            if (_curator.User != null)
            {
                _curator.Name = _curator.User.Name;
                _curator.User.Role = EnumRoles.Curator;
                _curator.User.Division = null;
            }

            var responseUpdateCurator = await CuratorService.UpdateAsync(_curator);
            if (_curator.User != null)
            {
                var responseUpdateUser = await UserService.UpdateAsync(_curator.User);

                if (responseUpdateCurator.IsSuccessStatusCode && responseUpdateUser.IsSuccessStatusCode)
                    await MessageService.Success($"Пользователь {_curator.Name} успешно добавлен");
                else
                    await MessageService.Error($"{responseUpdateCurator.ReasonPhrase}\n{responseUpdateUser.ReasonPhrase}");
            }
        }

        _isLoading = false;
        StateHasChanged();

        NavigationManager.NavigateTo("/curator");
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