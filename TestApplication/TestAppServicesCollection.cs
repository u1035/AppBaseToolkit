using AppBaseToolkit.AppBase;
using DiskLogger;

namespace TestApplication;

public class TestAppServicesCollection : ServicesCollection
{
    /// <inheritdoc />
    public TestAppServicesCollection(LogManager logManager) : base(logManager)
    {
    }
}