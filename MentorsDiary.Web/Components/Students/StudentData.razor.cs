using MentorsDiary.Application.Entities.Students.Domains;
using Microsoft.AspNetCore.Components;

namespace MentorsDiary.Web.Components.Students;

/// <summary>
/// Class StudentData.
/// Implements the <see cref="ComponentBase" />
/// </summary>
/// <seealso cref="ComponentBase" />
public partial class StudentData
{
    /// <summary>
    /// Gets or sets the student.
    /// </summary>
    /// <value>The student.</value>
    [Parameter]
    public Student? Student { get; set; }

    /// <summary>
    /// Gets or sets the student changed.
    /// </summary>
    /// <value>The student changed.</value>
    [Parameter]
    public EventCallback<Student>? StudentChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="StudentData" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [Parameter]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the change visible.
    /// </summary>
    /// <value>The change visible.</value>
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Closes the student page.
    /// </summary>
    private void CloseStudentPage()
    {
        Visible = !Visible;
    }
}