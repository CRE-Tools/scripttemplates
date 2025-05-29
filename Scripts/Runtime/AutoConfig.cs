using System.IO;
using UnityEditor;
using UnityEngine;

namespace PUCPR.ScriptTemplates
{
    [InitializeOnLoad]
    public class AutoConfig : MonoBehaviour
    {
        const string _settingsPath = "Packages/com.pucpr.scripttemplates/Settings/Settings_ScriptTemplates.asset";
        const string _importFilesPath = "Packages/com.pucpr.scripttemplates/Scripts/ScriptTemplates/";
        const string _dirPath = "Assets/ScriptTemplates/";

        static SO_ToolSettings _settings;

        static AutoConfig()
        {
            GetSettings();

            string logResume = "Template Files Verify ";
            string log = string.Empty;

            var filesResult = GetFiles();
            if (filesResult.assetNames.Length <= 0) return;

            var directoryResult = CreateDirectory();
            if (directoryResult.created)
                log += directoryResult.log;

            log += $"{filesResult.log}\r\n";

            var importResult = ImportFiles(filesResult.assetNames);
            log += importResult.log;

            logResume += Status(importResult.newFiles);
            logResume += log;

            PrintResults(importResult.newFiles, logResume);
        }


        static void GetSettings()
        {
            _settings = (SO_ToolSettings)AssetDatabase.LoadAssetAtPath(_settingsPath, typeof(SO_ToolSettings));
        }

        static string Status(bool hasNewFiles)
        {
            string status = hasNewFiles ? $" - Status: Updated = Please restart your project" : $" - Status: Ok";
            status += "\r\n\r\n";
            return status;
        }

        static (string[] assetNames, string log) GetFiles()
        {
            var guids = AssetDatabase.FindAssets("*", new[] { _importFilesPath });

            string[] assetNames = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assetNames[i] = Path.GetFileName(assetPath);
            }

            return (assetNames, $"Template files verified: {guids.Length}\r\n");
        }

        static (bool created, string log) CreateDirectory()
        {
            if (!Directory.Exists(_dirPath))
            {
                Directory.CreateDirectory(_dirPath);
                return (true, $"ScriptTemplates Folder created\r\n\r\n");
            }
            return (false, string.Empty);
        }

        static (bool newFiles, string log) ImportFiles(string[] data)
        {
            string importedLog = string.Empty;
            string non_ImportedLog = string.Empty;
            string log = string.Empty;

            foreach (var file in data)
            {
                var callback = CopyFileToDir(file);

                if (callback.created)
                    importedLog += $"{callback.fileName}\r\n";
                else
                    non_ImportedLog += $"{callback.fileName}\r\n";
            }

            if (importedLog != string.Empty)
                log += $"Imported files:\r\n{importedLog}\r\n";
            if (non_ImportedLog != string.Empty)
                log += $"Already imported files:\r\n{non_ImportedLog}\r\n";

            return (importedLog != string.Empty, log);
        }

        static (bool created, string fileName) CopyFileToDir(string fileName)
        {
            string importFilePath = $"{_importFilesPath}{fileName}";
            string dirFilePath = $"{_dirPath}{fileName}";

            if (!File.Exists(dirFilePath))
            {
                File.Copy(importFilePath, dirFilePath);
                return (true, fileName);
            }

            return (false, fileName);
        }

        static void PrintResults(bool status, string configLog)
        {
            if (_settings)
            {
                if (!_settings.showdebugs)
                    return;
            }

            if (status)
                Debug.LogWarning(configLog);
            else
                Debug.Log(configLog);
        }
    }

}
