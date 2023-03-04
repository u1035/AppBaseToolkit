using System.ComponentModel;
using AppBaseToolkit.Attributes;
using AppBaseToolkit.ConfigurationStoring;
using AppBaseToolkit.Mvvm;
using JetBrains.Annotations;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Base class for application config
/// </summary>
[PublicAPI]
public abstract class ApplicationConfigBase : NotificationObject
{
    private bool _loaded;

    /// <summary>
    /// Position of main window
    /// </summary>
    [Store]
    public WindowPosition MainWindowPosition { get; } = new();

    #region IsDarkThemeSelected property

    /// <summary>
    /// IsDarkThemeSelected property
    /// </summary>
    [Store]
    public bool IsDarkThemeSelected
    {
        get => _isDarkThemeSelected;
        set => SetProperty(ref _isDarkThemeSelected, value);
    }

    private bool _isDarkThemeSelected;

    #endregion




    /// <summary>
    /// Initializes a new instance of <see cref="ApplicationConfigBase"/>
    /// </summary>
    [PublicAPI]
    protected ApplicationConfigBase()
    {
        MainWindowPosition.PropertyChanged += OnConfigUpdated; //saving window position when it's changed
        PropertyChanged += OnConfigUpdated; //saving other updated properties
    }

    private void OnConfigUpdated(object? sender, PropertyChangedEventArgs e)
    {
        if (_loaded)
            SaveToDisk();
    }

    /// <summary>
    /// Operations performed with config after it's loaded from disk
    /// </summary>
    public virtual void AfterLoaded()
    {
        _loaded = true;
    }

    /// <summary>
    /// Stores config to disk
    /// </summary>
    public void SaveToDisk()
    {
        UserDataStorage.StoreUserData(this, Workspace.AppConfigFileName);
    }
}