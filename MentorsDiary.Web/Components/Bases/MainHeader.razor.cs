using HttpService.Services;
using MentorsDiary.Application.Entities.Users.Domains;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Bases;

/// <summary>
/// Class MainHeader.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class MainHeader
{
    /// <summary>
    /// Gets or sets the authentication service.
    /// </summary>
    /// <value>The authentication service.</value>
    [Inject]
    private AuthenticationService AuthenticationService { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <value>The current user.</value>
    private User CurrentUser => (User)AuthenticationService.AuthorizedUser!;

    /// <summary>
    /// Navigates to login window.
    /// </summary>
    public void NavigateToLoginWindow()
    {
        NavigationManager.NavigateTo("/login");
    }

    /// <summary>
    /// Navigates to home.
    /// </summary>
    private void NavigateToHome()
    {
        NavigationManager.NavigateTo("/");
    }
}