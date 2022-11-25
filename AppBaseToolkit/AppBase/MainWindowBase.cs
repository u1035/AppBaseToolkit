using System.Windows;
using System.Windows.Data;
using JetBrains.Annotations;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Base class for main window, manages saving and loading of window position
/// </summary>
[PublicAPI]
public class MainWindowBase<T> : Window where T : MainWindowViewModelBase
{
    /// <summary>
    /// ViewModel of main window
    /// </summary>
    protected T Model { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="MainWindowBase{T}"/>
    /// </summary>
    /// <param name="model"></param>
    protected MainWindowBase(T model)
    {
        Model = model;
        DataContext = model;

        BindingOperations.SetBinding(this, WindowStateProperty, new Binding(nameof(WindowPosition.WindowState))
        {
            Source = model.WindowPosition, 
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
        BindingOperations.SetBinding(this, LeftProperty, new Binding(nameof(WindowPosition.WindowLeft))
        {
            Source = model.WindowPosition,
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
        BindingOperations.SetBinding(this, TopProperty, new Binding(nameof(WindowPosition.WindowTop))
        {
            Source = model.WindowPosition, 
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
        BindingOperations.SetBinding(this, HeightProperty, new Binding(nameof(WindowPosition.WindowHeight))
        {
            Source = model.WindowPosition, 
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
        BindingOperations.SetBinding(this, WidthProperty, new Binding(nameof(WindowPosition.WindowWidth))
        {
            Source = model.WindowPosition, 
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
    }
}