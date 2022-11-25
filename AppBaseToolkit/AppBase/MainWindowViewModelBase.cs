using System.Windows;
using System.Windows.Threading;
using AdonisUI;
using AppBaseToolkit.Mvvm;
using JetBrains.Annotations;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Base viewmodel for main app window
/// </summary>
[PublicAPI]
public class MainWindowViewModelBase : NotificationObject
{
    /// <summary>
    /// Main window position and state
    /// </summary>
    public WindowPosition WindowPosition { get; }

    /// <summary>
    /// Main UI thread
    /// </summary>
    protected Dispatcher Dispatcher { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="MainWindowViewModelBase"/>
    /// </summary>
    /// <param name="config"></param>
    protected MainWindowViewModelBase(ApplicationConfigBase config)
    {
        WindowPosition = config.MainWindowPosition;

        SetColorTheme(config.IsDarkThemeSelected);
        config.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(ApplicationConfigBase.IsDarkThemeSelected))
                SetColorTheme(config.IsDarkThemeSelected);
        };

        Dispatcher = Dispatcher.CurrentDispatcher;
    }
    
    private static void SetColorTheme(bool isDarkThemeSelected)
    {
        ResourceLocator.SetColorScheme(Application.Current.Resources,
            isDarkThemeSelected
                ? ResourceLocator.DarkColorScheme
                : ResourceLocator.LightColorScheme);
    }
}