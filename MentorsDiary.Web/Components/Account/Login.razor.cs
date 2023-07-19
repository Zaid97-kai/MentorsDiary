using AntDesign;
using MentorsDiary.Web.Data.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Account;

public partial class Login
{
    #region PARAMETERS

    /// <summary>
    /// Gets or sets a value indicating whether this is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [Parameter]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the change visible.
    /// </summary>
    /// <value>The change visible.</value>
    [Parameter]
    public EventCallback<bool> ChangeVisible { get; set; }

    #endregion

    #region INJECTIONS

    /// <summary>
    /// Gets or sets the navigation manager.
    /// </summary>
    /// <value>The navigation manager.</value>
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    /// <summary>
    /// Gets or sets the website authenticator.
    /// </summary>
    /// <value>The website authenticator.</value>
    [Inject]
    private WebsiteAuthenticator WebsiteAuthenticator { get; set; } = null!;

    /// <summary>
    /// Gets or sets the message service.
    /// </summary>
    /// <value>The message service.</value>
    [Inject]
    private IMessageService MessageService { get; set; } = null!;

    #endregion

    /// <summary>
    /// Gets or sets the login form model.
    /// </summary>
    /// <value>The login form model.</value>
    private LoginFormModel LoginFormModel { get; set; } = new();
    
    private async void TryLogin()
    {
        var result = await WebsiteAuthenticator.LoginAsync(LoginFormModel);
        if (result)
        {
            await MessageService.Success("Вход выполнен успешно")!;
            NavigationManager.NavigateTo("/", true);
        }
        else
        {
            await MessageService.Error("Ошибка входа")!;
        }

        StateHasChanged();
    }
    
    private async void TryLogout()
    {
        await WebsiteAuthenticator?.LogoutAsync()!;
        NavigationManager?.NavigateTo("/", true);
        StateHasChanged();
    }
}