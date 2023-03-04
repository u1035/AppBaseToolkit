using System;
using System.IO;
using System.Windows;
using AppBaseToolkit.ConfigurationStoring;
using DiskLogger;
using JetBrains.Annotations;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Manages application startup initialization
/// </summary>
[PublicAPI]
public abstract class ApplicationBase<TAppConfig, TAppServices> : Application 
    where TAppConfig : ApplicationConfigBase
{
    private readonly LogManager _logManager;

    /// <summary>
    /// Initializes a new instance of Application
    /// </summary>
    protected ApplicationBase()
    {
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/AdonisUI;component/ColorSchemes/Light.xaml", UriKind.RelativeOrAbsolute) });
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/AdonisUI;component/ColorSchemes/Dark.xaml", UriKind.RelativeOrAbsolute) });
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/AdonisUI.ClassicTheme;component/Resources.xaml", UriKind.RelativeOrAbsolute) });

        Workspace.Initialize();
        _logManager = new LogManager(Workspace.LogsFolder, Workspace.AppName);
        StartupSequence();
    }

    private void StartupSequence()
    {
        var applicationConfig = CreateDefaultConfig();
        LoadConfig(applicationConfig);
        var servicesCollection = InitServices(applicationConfig, _logManager);
        ShowMainWindow(applicationConfig, servicesCollection);
    }

    private static void LoadConfig(TAppConfig config)
    {
        if (!File.Exists(Workspace.AppConfigFileName))
            config.SaveToDisk();
        else
            UserDataStorage.LoadUserData(config, Workspace.AppConfigFileName);

        config.AfterLoaded();
    }

    /// <summary>
    /// Create default config
    /// </summary>
    /// <returns></returns>
    protected abstract TAppConfig CreateDefaultConfig();

    /// <summary>
    /// Initializes app services
    /// </summary>
    /// <param name="config"></param>
    /// <param name="logManager"></param>
    /// <returns></returns>
    protected abstract TAppServices InitServices(TAppConfig config, LogManager logManager);

    /// <summary>
    /// Creates main window view and model and displays window
    /// </summary>
    /// <param name="config"></param>
    /// <param name="servicesCollection"></param>
    protected abstract void ShowMainWindow(TAppConfig config, TAppServices servicesCollection);
}