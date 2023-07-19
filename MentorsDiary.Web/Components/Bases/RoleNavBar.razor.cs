using HttpService.Services;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Bases;

/// <summary>
/// Class RoleNavBar.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class RoleNavBar
{
    /// <summary>
    /// Gets or sets the service.
    /// </summary>
    /// <value>The service.</value>
    [Inject]
    private AuthenticationService? Service { get; set; }

    /// <summary>
    /// Gets or sets the style nav bar for groups.
    /// </summary>
    /// <value>The style nav bar for groups.</value>
    private string? StyleNavBarForGroups { get; set; } = "nav-item";

    /// <summary>
    /// Gets or sets the style nav bar for curator.
    /// </summary>
    /// <value>The style nav bar for curator.</value>
    private string? StyleNavBarForCurator { get; set; } = "nav-item";

    /// <summary>
    /// Gets or sets the style nav bar for stats.
    /// </summary>
    /// <value>The style nav bar for stats.</value>
    private string? StyleNavBarForStats { get; set; } = "nav-item";

    /// <summary>
    /// Gets or sets the style nav bar for mero.
    /// </summary>
    /// <value>The style nav bar for mero.</value>
    private string? StyleNavBarForMero { get; set; } = "nav-item";

    /// <summary>
    /// Gets or sets the style nav bar for deputy director.
    /// </summary>
    /// <value>The style nav bar for deputy director.</value>
    private string? StyleNavBarForDeputyDirector { get; set; } = "nav-item";

    /// <summary>
    /// Sets the style for groups.
    /// </summary>
    private void SetStyleForGroups()
    {
        StyleNavBarForGroups = "nav-item nav-item-in";
        StyleNavBarForCurator = "nav-item";
        StyleNavBarForStats = "nav-item";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for curator.
    /// </summary>
    private void SetStyleForCurator()
    {
        StyleNavBarForCurator = "nav-item nav-item-in";
        StyleNavBarForGroups = "nav-item";
        StyleNavBarForStats = "nav-item";
        StyleNavBarForMero = "nav-item";
        StyleNavBarForDeputyDirector = "nav-item";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for stats.
    /// </summary>
    private void SetStyleForStats()
    {
        StyleNavBarForStats = "nav-item nav-item-in";
        StyleNavBarForCurator = "nav-item";
        StyleNavBarForGroups = "nav-item";
        StyleNavBarForMero = "nav-item";
        StyleNavBarForDeputyDirector = "nav-item";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for mero.
    /// </summary>
    private void SetStyleForMero()
    {
        StyleNavBarForMero = "nav-item nav-item-in";
        StyleNavBarForGroups = "nav-item";
        StyleNavBarForStats = "nav-item";
        StyleNavBarForDeputyDirector = "nav-item";
        StyleNavBarForCurator = "nav-item";

        StateHasChanged();
    }

    /// <summary>
    /// Sets the style for deputy director.
    /// </summary>
    private void SetStyleForDeputyDirector()
    {
        StyleNavBarForDeputyDirector = "nav-item nav-item-in";
        StyleNavBarForCurator = "nav-item";
        StyleNavBarForGroups = "nav-item";
        StyleNavBarForMero = "nav-item";
        StyleNavBarForStats = "nav-item";

        StateHasChanged();
    }
}