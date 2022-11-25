using System;

namespace AppBaseToolkit.Attributes
{
    /// <summary>
    /// Fields marked with this attribute saved to application config file with UserDataStorage.StoreUserData and loaded with UserDataStorage.LoadUserData
    /// </summary>
    public class StoreAttribute : Attribute
    {
    }
}
