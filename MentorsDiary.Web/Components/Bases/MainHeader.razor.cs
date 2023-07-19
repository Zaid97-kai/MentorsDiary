using HttpService.Services;
using MentorsDiary.Application.Entities.Users.Domains;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Bases;

public partial class MainHeader
{
    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is visible modal.
    /// </summary>
    /// <value><c>true</c> if this instance is visible modal; otherwise, <c>false</c>.</value>
    private bool IsVisibleEnterModalWindow { get; set; }

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService?.AuthorizedUser!;

    public void NavigateToLoginWindow()
    {
        NavigationManager.NavigateTo("/login");

        StateHasChanged();
    }

    private void NavigateToHome()
    {
        NavigationManager.NavigateTo("/");
    }
}