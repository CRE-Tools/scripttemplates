using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PUCPR.ScriptTemplates.Editor
{
    public class MultiScriptCreator : EditorWindow
    {
        static private string finalPath = "";
        static List<string> scriptTemplates;

        private string _name = "";
        private bool useCustomPaths = false;
        private string[] customPaths;

        private const string msc_Path = "Assets/MultiScriptCreator";
        private const string type_txt = ".txt";
        private const string type_cSharp = ".cs";


        [MenuItem("Assets/Create Script/Multi Script Creator", priority = 11)]
        static void MultiCodeGen()
        {
            if (ValidateDirectory(msc_Path))
            {
                AssetDatabase.Refresh();
            }

            if (GetFiles())
            {
                if (GetActiveFolderPath())
                    ShowMyEditor();
            }
            else
            {
                Debug.LogWarning($"{msc_Path} is empty");
            }
        }

        private static bool GetFiles()
        {
            scriptTemplates = new List<string>();
            string[] files = Directory.GetFiles(msc_Path, "*.txt");

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            scriptTemplates.AddRange(files);

            return scriptTemplates.Count > 0;
        }

        private static bool GetActiveFolderPath()
        {
            var _tryGetActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("TryGetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);

            object[] args = new object[] { null };
            bool found = (bool)_tryGetActiveFolderPath.Invoke(null, args);
            finalPath = (string)args[0] + "/";

            return found;
        }

        public static void ShowMyEditor()
        {
            EditorWindow wnd = GetWindow<MultiScriptCreator>();
            wnd.titleContent = new GUIContent("Multi Code Generator");

            wnd.minSize = new Vector2(450, 200);
            wnd.maxSize = new Vector2(1920, 720);
        }

        private void CreateGUI()
        {
            Array.Resize(ref customPaths, scriptTemplates.Count);
        }

        private void OnGUI()
        {
            _GUI_Header();
            _GUI_Body();
            _GUI_Footer();
        }

        #region Header
        private void _GUI_Header()
        {

        }

        #endregion

        #region Body
        private void _GUI_Body()
        {
            Inputs();

            Submit();
        }

        private void Inputs()
        {
            GUILayout.Label("Name for files:");
            _name = GUILayout.TextField(_name);
            GUILayout.Space(8);

            Inputs_CustomPaths();
        }

        private void Inputs_CustomPaths()
        {
            useCustomPaths = GUILayout.Toggle(useCustomPaths, "Use Custom Paths");

            if (useCustomPaths)
            {
                for (int i = 0; i < scriptTemplates.Count; i++)
                {
                    GUILayout.Label(scriptTemplates[i]);
                    customPaths[i] = finalPath + scriptTemplates[i] + "/";
                    customPaths[i] = GUILayout.TextField(customPaths[i]);
                }
            }
        }

        private void Submit()
        {
            if (GUILayout.Button("Generate Files"))
            {
                GenerateFiles();
                this.Close();
            }

            GUILayout.Space(12);
        }

        #endregion

        #region Footer
        private void _GUI_Footer()
        {

        }

        #endregion


        private void GenerateFiles()
        {

            for (int i = 0; i < scriptTemplates.Count; i++)
            {
                string path = useCustomPaths ? customPaths[i] : finalPath;
                string className = _name + scriptTemplates[i];
                string file = path + className + type_cSharp;


                if (!FileExists(file, className))
                {
                    ValidateDirectory(path);

                    File.WriteAllText(file, GetCode(scriptTemplates[i]));

                    Debug.Log($"Generated file: {file}");
                }
                else
                {
                    Debug.LogWarning($"{className}{type_cSharp} already exists!!!");
                }
            }

            AssetDatabase.Refresh();
        }

        private bool FileExists(string filePath, string className)
        {
            if (File.Exists(filePath))
                return true;

            if (ClassExists(className))
                return true;

            return false;
        }

        private static bool ValidateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }

            return false;
        }

        private string GetCode(string file)
        {
            var textAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(msc_Path + file + type_txt, typeof(TextAsset));
            string text = textAsset.text;

            text = text.Replace("#NOTRIM#", "");
            text = text.Replace("#SCRIPTNAME#", _name);

            return text;
        }

        public static bool ClassExists(string className)
        {
            var csFiles = Directory.GetFiles("Assets/", "*.cs", SearchOption.AllDirectories);

            foreach (var file in csFiles)
            {
                string content = File.ReadAllText(file);
                var classMatch = Regex.Match(content, $@"\b(class|struct|interface|record)\s+{Regex.Escape(className)}\b");

                if (classMatch.Success)
                    return true;
            }

            return false;
        }
    }
}
