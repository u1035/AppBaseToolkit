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

}