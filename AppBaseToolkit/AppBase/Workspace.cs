using System.Diagnostics;
using System.IO;
using System;
using JetBrains.Annotations;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Application workspace
/// </summary>
[PublicAPI]
public static class Workspace
{
#nullable disable //Initialize() must be called on application start
    /// <summary>
    /// Application files folder, default MyDocuments/{ProcessName}
    /// </summary>
    public static string AppFolder { get; private set; }

    /// <summary>
    /// Name of application
    /// </summary>
    public static string AppName { get; private set; }

    /// <summary>
    /// Main app config file
    /// </summary>
    public static string AppConfigFileName { get; private set; }

    /// <summary>
    /// Application setting folder
    /// </summary>
    public static string SettingsFolder { get; private set; }

    /// <summary>
    /// Application logs folder
    /// </summary>
    public static string LogsFolder { get; private set; }

#nullable enable

    /// <summary>
    /// Application logs folder
    /// </summary>
    public static Version? AppVersion { get; set; }

    /// <summary>
    /// Application workspace constructor
    /// </summary>
    public static void Initialize()
    {
        AppName = Process.GetCurrentProcess().ProcessName;
        AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), AppName);
        Directory.CreateDirectory(AppFolder);
        SettingsFolder = InitializeFolder("settings");
        AppConfigFileName = Path.Combine(SettingsFolder, "settings.json");
        LogsFolder = InitializeFolder("logs");
    }

    private static string InitializeFolder(string folder)
    {
        var path = Path.Combine(AppFolder, folder);
        Directory.CreateDirectory(path);
        return path;
    }
}