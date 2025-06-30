using UnityEditor;
using static PUCPR.ScriptTemplates.Editor.Static_Variables;

namespace PUCPR.ScriptTemplates.Editor
{
    public static partial class ScriptCreator
    {

#if _
        [MenuItem(MENU_PATH_INTERN + _00INTERN, priority = 1)]
        static void CreateIntern() => CreateScriptFile(_00INTERN);
#endif

        private static void CreateScriptFile(string fileName)
        {
            string templatePath = $"{FILE_PATH_BASE}{fileName}.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, $"New{fileName}.cs");
        }

    }
}
