using DiskLogger;

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Overrides of ApplicationBase

        /// <inheritdoc />
        protected override TestAppConfig CreateDefaultConfig() => new();

        /// <inheritdoc />
        protected override TestAppServicesCollection InitServices(TestAppConfig config, LogManager logManager)
        {
            return new TestAppServicesCollection(logManager);
        }

        /// <inheritdoc />
        protected override void ShowMainWindow(TestAppConfig config, TestAppServicesCollection servicesCollection)
        {
            var mainWindowViewModel = new MainWindowViewModel(config, servicesCollection);
            var mainWindow = new MainWindow(mainWindowViewModel);
            mainWindow.Show();
        }

        #endregion
    }
}
