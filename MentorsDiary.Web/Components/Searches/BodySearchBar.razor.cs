using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Web.Data.Services.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MentorsDiary.Web.Components.Searches;

/// <summary>
/// Class BodySearchBar.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <typeparam name="TValue">The type of the t value.</typeparam>
/// <typeparam name="TService">The type of the t service.</typeparam>
/// <seealso cref="ComponentBase" />
public partial class BodySearchBar<TValue, TService>
    where TValue : class, IHaveName, new()
    where TService : BaseService<TValue>
{
    /// <summary>
    /// Gets or sets the on selected item changed handler.
    /// </summary>
    /// <value>The on selected item changed handler.</value>
    [Parameter]
    public EventCallback<TValue?> OnSelectedItemChangedHandler { get; set; }

    /// <summary>
    /// Gets or sets the on search item changed handler.
    /// </summary>
    /// <value>The on search item changed handler.</value>
    [Parameter]
    public EventCallback<string?> OnSearchItemChangedHandler { get; set; }

    /// <summary>
    /// Gets or sets the division service.
    /// </summary>
    /// <value>The division service.</value>
    [Inject]
    private TService? Service { get; set; }

    /// <summary>
    /// Gets or sets the values.
    /// </summary>
    /// <value>The values.</value>
    private List<TValue>? Values { get; set; }

    /// <summary>
    /// Gets or sets the selected value.
    /// </summary>
    /// <value>The selected value.</value>
    private string? SelectedValue { get; set; }

    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    /// <value>The text value.</value>
    public string? TxtValue { get; set; }

    /// <summary>
    /// On initialized as an asynchronous operation.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        var ret = await Service?.GetAllAsync()!;

        Values = new List<TValue>();
        Values?.Add(new TValue() { Name = "Все" });
        Values?.AddRange((ret ?? Array.Empty<TValue>()).ToList());
    }

    /// <summary>
    /// Called when [blur].
    /// </summary>
    private void OnBlur()
    {

    }

    /// <summary>
    /// Called when [focus].
    /// </summary>
    /// <returns>Task.</returns>
    private Task OnFocus()
    {
        Console.WriteLine("focus");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Called when [search].
    /// </summary>
    /// <param name="obj">The object.</param>
    private void OnSearch(string obj)
    {
        Console.WriteLine($"search: {obj}");
    }

    /// <summary>
    /// Called when [search].
    /// </summary>
    public async Task OnSearch()
    {

    }

    /// <summary>
    /// Handles the <see cref="E:Enter" /> event.
    /// </summary>
    /// <param name="e">The <see cref="KeyboardEventArgs" /> instance containing the event data.</param>
    private async Task OnEnter(KeyboardEventArgs e)
    {

    }
}