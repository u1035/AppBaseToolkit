using System.Reflection;
using System;
using AppBaseToolkit.AppBase;
using DiskLogger;

namespace TestApplication;

public class MainWindowViewModel : MainWindowViewModelBase
{
    private readonly TestAppServicesCollection _servicesCollection;
    private Logger Logger => _servicesCollection.LogManager.ForContext<MainWindowViewModel>();

    public TestAppConfig Config { get; }
    public string WindowHeader { get; }


    /// <summary>
    /// Initializes a new instance of <see cref="MainWindowViewModel"/>
    /// </summary>
    /// <param name="config"></param>
    /// <param name="servicesCollection"></param>
    public MainWindowViewModel(TestAppConfig config, TestAppServicesCollection servicesCollection) : base(config)
    {
        _servicesCollection = servicesCollection;
        Config = config;

        Workspace.AppVersion = Assembly.GetExecutingAssembly().GetName().Version;
        WindowHeader = $"Test application ver. {Workspace.AppVersion}";
        Logger.Info($"{Workspace.AppName} {Workspace.AppVersion} started at {DateTime.Now}.");
    }
}