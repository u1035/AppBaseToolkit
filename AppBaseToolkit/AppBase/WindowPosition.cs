using System.Windows;
using AppBaseToolkit.Attributes;
using AppBaseToolkit.Mvvm;

namespace AppBaseToolkit.AppBase;

/// <summary>
/// Contains information about window position and state
/// </summary>
public class WindowPosition : NotificationObject
{
    #region WindowWidth property

    /// <summary>
    /// WindowWidth property
    /// </summary>
    [Store]
    public int WindowWidth
    {
        get => _windowWidth;
        set => SetProperty(ref _windowWidth, value);
    }

    private int _windowWidth;

    #endregion

    #region WindowHeight property

    /// <summary>
    /// WindowHeight property
    /// </summary>
    [Store]
    public int WindowHeight
    {
        get => _windowHeight;
        set => SetProperty(ref _windowHeight, value);
    }

    private int _windowHeight;

    #endregion

    #region WindowTop property

    /// <summary>
    /// WindowTop property
    /// </summary>
    [Store]
    public int WindowTop
    {
        get => _windowTop;
        set => SetProperty(ref _windowTop, value);
    }

    private int _windowTop;

    #endregion

    #region WindowLeft property

    /// <summary>
    /// WindowLeft property
    /// </summary>
    [Store]
    public int WindowLeft
    {
        get => _windowLeft;
        set => SetProperty(ref _windowLeft, value);
    }

    private int _windowLeft;

    #endregion

    #region WindowState property

    /// <summary>
    /// WindowState property
    /// </summary>
    [Store]
    public WindowState WindowState
    {
        get => _windowState;
        set => SetProperty(ref _windowState, value);
    }

    private WindowState _windowState;

    #endregion


    /// <summary>
    /// Initializes a new instance of <see cref="WindowPosition"/>
    /// </summary>
    /// <param name="windowWidth"></param>
    /// <param name="windowHeight"></param>
    /// <param name="windowTop"></param>
    /// <param name="windowLeft"></param>
    /// <param name="windowState"></param>
    public WindowPosition(int windowWidth, int windowHeight, int windowTop, int windowLeft, WindowState windowState)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _windowTop = windowTop;
        _windowLeft = windowLeft;
        _windowState = windowState;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="WindowPosition"/>with default values
    /// </summary>
    public WindowPosition()
    {
        _windowHeight = 1000;
        _windowWidth = 1600;
        _windowLeft = 10;
        _windowTop = 10;
        _windowState = WindowState.Normal;
    }
}