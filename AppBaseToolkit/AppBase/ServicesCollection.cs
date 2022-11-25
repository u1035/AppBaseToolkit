using DiskLogger;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Base class for services
/// </summary>
public class ServicesCollection
{
    public LogManager LogManager { get; }

    protected ServicesCollection(LogManager logManager)
    {
        LogManager = logManager;
    }
}