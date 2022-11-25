using System;
using System.Globalization;
using System.IO;
using System.Linq;
using AppBaseToolkit.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppBaseToolkit.ConfigurationStoring
{
    /// <summary>
    /// Base class for JSON serialization
    /// </summary>
    public static class JsonStorage
    {
        /// <summary>
        /// Loading object's parameters marked with [Store] attribute from config file (filename =  WorkSpace.SettingsFolder + {TypeName -or- fileName}.xml)
        /// </summary>
        /// <param name="obj">Object, which parameters marked with [Store] attribute needed to load</param>
        /// <param name="filePath">FullFile name to load data</param>
        /// <param name="contractResolver"></param>
        public static void LoadUserData(object obj, string filePath, DefaultContractResolver contractResolver)
        {
            try
            {
                if (!File.Exists(filePath))
                    return;

                var jsonText = File.ReadAllText(filePath);
                var serializer = new JsonSerializer { ContractResolver = contractResolver };
                serializer.Populate(new JsonTextReader(new StringReader(jsonText)), obj);
            }
            catch (Exception)
            {
                /* ignored*/
            }
        }

        /// <summary>
        /// Storing object's parameters marked with [Store] attribute to config file (filename =  WorkSpace.SettingsFolder + {TypeName -or- fileName}.xml)
        /// </summary>
        /// <param name="obj">Object which parameters marked [Store] attribute needs to be restored</param>
        /// <param name="filePath">Full file name to store data</param>
        /// <param name="contractResolver">Contract resolver. to avoid backing fields should be null</param>
        /// <param name="doBackup">Create backup file</param>
        public static void StoreUserData(object obj, string filePath, DefaultContractResolver? contractResolver, bool doBackup = false)
        {
            var folder = Path.GetDirectoryName(filePath);
            if (folder.IsNotNullOrEmpty() && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (doBackup)
                MakeBackup(filePath, folder ?? Environment.CurrentDirectory);

            var settings = new JsonSerializerSettings { ContractResolver = contractResolver };
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception)
            {
                /* ignored*/
            }
        }


        #region Config Backup

        //version 1. May contain bugs.
        //todo: backup rotation performed on each StoreUserData() call, so if your program stores settings on every change (not only on exit) - yesterday backups will be overwritten soon

        /// <summary>
        /// Maximum number of copies to keep. Oldest copy removed on new backup creation.                                                                           <br/>
        /// With NumberOfBackupsToKeep = 5 settings folder will contain (for example):                                                                              <br/>
        /// MainViewModel.json          -   latest backup that <see cref="StoreUserData"/> will make (will be renamed to .bak.1 on next program exit)               <br/>
        /// MainViewModel.json.bak.1    -   previous version (will be renamed to .bak.2 on next program exit)                                                       <br/>
        /// MainViewModel.json.bak.2    -   will be renamed to .bak.3 on next program exit                                                                          <br/>
        /// MainViewModel.json.bak.3    -   will be renamed to .bak.4 on next program exit                                                                          <br/>
        /// MainViewModel.json.bak.4    -   will be renamed to .bak.5 on next program exit                                                                          <br/>
        /// MainViewModel.json.bak.5    -   oldest version (f.e. 5 program exits ago), will be removed on next program exit
        /// </summary>
        private const int NumberOfBackupsToKeep = 5;

        /// <summary>
        /// Manages backups - removes oldest files and increases backup number (renames) on existing.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="folder"></param>
        private static void MakeBackup(string filePath, string folder)
        {
            try
            {
                //Making search pattern like "MainViewModel.*"
                var configFileName = Path.GetFileNameWithoutExtension(filePath) + ".*";

                //we getting list of existing config and backup files and remove oldest backups, if needed
                //sorting it from oldest backup (".5") to current config (".json")
                var existingBackups = Directory.GetFiles(folder, configFileName, SearchOption.TopDirectoryOnly)
                    .OrderByDescending(x => x);
                foreach (var existingBackup in existingBackups)
                {
                    var backupNumber = GetBackupNumber(existingBackup);
                    if (backupNumber > NumberOfBackupsToKeep - 1)
                    {
                        File.Delete(existingBackup);
                    }
                    else
                    {
                        var newFileName = ReplaceBackupNumber(existingBackup, backupNumber + 1);
                        File.Move(existingBackup, newFileName);
                    }
                }
            }
            catch (Exception)
            {
                //ignored, breakpoint lives here
            }
        }

        /// <summary>
        /// Analyzes file name and returns backup number.                                                                           <br/>
        /// For "MainViewModel.json.bak.1" it returns 1, for "MainViewModel.json.bak.5" - 5.                                        <br/>
        /// For current config file (like "MainViewModel.json") returns 0                                                           <br/>
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static int GetBackupNumber(string filename)
        {
            try
            {
                var lastPointIndex = filename.LastIndexOf('.');
                if (lastPointIndex > -1)
                {
                    var stringBackupNumber = filename.Substring(lastPointIndex + 1, filename.Length - lastPointIndex - 1);
                    if (int.TryParse(stringBackupNumber, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
                        return result;
                }
            }
            catch (Exception)
            {
                //breakpoint lives here
                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Performs string operations to modify backup number of <paramref name="filename"/> to new <paramref name="newNumber"/>.                              <br/>
        /// if <paramref name="filename"/> is "MainViewModel.json.bak.1" and <paramref name="newNumber"/> is 2 - returns "MainViewModel.json.bak.2"             <br/>
        /// if it's not backup file but current config file "MainViewModel.json" and <paramref name="newNumber"/> is 1 - returns "MainViewModel.json.bak.1"     <br/>
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="newNumber"></param>
        /// <returns></returns>
        private static string ReplaceBackupNumber(string filename, int newNumber)
        {
            try
            {
                //if newNumber = 1 we assume that it's first backup and filename not ends with ".bak.?" (f.e. "MainViewModel.json")
                if (newNumber == 1)
                {
                    return filename + ".bak." + newNumber.ToString(NumberFormatInfo.InvariantInfo);
                }
                else
                {
                    //we detecting last point in file name ("MainViewModel.json.bak.1") and assume that it's a backup number after that point
                    var lastPointIndex = filename.LastIndexOf('.');
                    if (lastPointIndex > -1)
                    {
                        var withoutNumber = filename.Substring(0, lastPointIndex + 1);
                        return withoutNumber + newNumber.ToString(NumberFormatInfo.InvariantInfo);
                    }
                }
            }
            catch (Exception)
            {
                //if something goes wrong - we just add new backup to end of file name
                //breakpoint lives here
                return filename + ".bak." + newNumber.ToString(NumberFormatInfo.InvariantInfo);
            }

            return filename + ".bak." + newNumber.ToString(NumberFormatInfo.InvariantInfo);
        }

        #endregion

    }


}
