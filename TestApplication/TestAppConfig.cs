using AppBaseToolkit.AppBase;
using AppBaseToolkit.Attributes;

namespace TestApplication;

public class TestAppConfig : ApplicationConfigBase
{
    #region SomeTestStringValue property

    /// <summary>
    /// SomeTestStringValue property
    /// </summary>
    [Store]
    public string? SomeTestStringValue
    {
        get => _someTestStringValue;
        set => SetProperty(ref _someTestStringValue, value);
    }

    private string? _someTestStringValue;

    #endregion
    
    #region SomeTestStringValue2 property

    /// <summary>
    /// SomeTestStringValue2 property
    /// </summary>
    [Store]
    public string? SomeTestStringValue2
    {
        get => _someTestStringValue2;
        set => SetProperty(ref _someTestStringValue2, value);
    }

    private string? _someTestStringValue2;

    #endregion

}