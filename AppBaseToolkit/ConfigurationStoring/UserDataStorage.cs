using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AppBaseToolkit.AppBase;
using AppBaseToolkit.Attributes;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppBaseToolkit.ConfigurationStoring
{
    /// <summary>
    /// Class provides methods for storing and loading fields marked with [Store] attribute to/from XML configuration file
    /// </summary>
    [PublicAPI]
    public static class UserDataStorage
    {
        /// <summary>
        /// Extension used for files
        /// </summary>
        private const string Extension = ".json";

        /// <summary>
        /// Loading object's parameters marked with [Store] attribute from config file (filename =  WorkSpace.SettingsFolder + {TypeName -or- fileName}.xml)
        /// </summary>
        /// <param name="obj">Object, which parameters marked with [Store] attribute needed to load</param>
        /// <param name="fileName">Optional, filename without extension, where to store object (if not set - typename used)</param>
        public static void LoadUserData(object obj, string? fileName = null)
        {
            var filePath = GetFullFileName(fileName ?? obj.GetType().Name);
            JsonStorage.LoadUserData(obj, filePath, new StoreAttributeResolver());
        }

        /// <summary>
        /// Storing object's parameters marked with [Store] attribute to config file (filename =  WorkSpace.SettingsFolder + {TypeName -or- fileName}.xml)
        /// </summary>
        /// <param name="obj">Object which parameters marked [Store] attribute needs to be restored</param>
        /// <param name="fileName">Optional, filename without extension, where object is stored (if not set - typename used)</param>
        /// <param name="doBackup">Create backup file</param>
        public static void StoreUserData(object obj, string? fileName = null, bool doBackup = false)
        {
            var filePath = GetFullFileName(fileName ?? obj.GetType().Name);
            JsonStorage.StoreUserData(obj, filePath, new StoreAttributeResolver(), doBackup);
        }

        /// <summary>
        /// Get full path of storage file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullFileName(string fileName)
        {
            if (!fileName.EndsWith(Extension))    //if filename is specified and already ends with extension - we don't add it
                fileName += Extension;

            return Path.Combine(Workspace.SettingsFolder, fileName);
        }
    }

    /// <summary>
    /// Helper class for JSON serializer for filtering properties and private fields marked with [Store] attribute
    /// </summary>
    public class StoreAttributeResolver : DefaultContractResolver
    {
        /// <summary>
        /// Return Serializable members
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "<Pending>")]
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            // ReSharper disable once PossibleNullReferenceException
            // ReSharper disable once CoVariantArrayConversion
            MemberInfo[] fields = objectType.GetFields(flags);
            return fields.Concat(objectType.GetProperties(flags)).Where(prop => prop.IsDefined(typeof(StoreAttribute), false)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return base.CreateProperties(type, MemberSerialization.Fields);
        }
    }
}
