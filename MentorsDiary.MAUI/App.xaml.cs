namespace MentorsDiary.MAUI;

/// <summary>
/// Class App.
/// Implements the <see cref="Microsoft.Maui.Controls.Application" />
/// </summary>
/// <seealso cref="Microsoft.Maui.Controls.Application" />
public partial class App : Microsoft.Maui.Controls.Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <remarks>To be added.</remarks>
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}