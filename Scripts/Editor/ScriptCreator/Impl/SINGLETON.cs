using UnityEditor;
using static PUCPR.ScriptTemplates.Editor.Static_Variables;

namespace PUCPR.ScriptTemplates.Editor
{
    public static partial class ScriptCreator
    {
        [MenuItem(MENU_PATH_BASE + SINGLETON, priority = 0)] 
        static void CreateSINGLETON() => CreateScriptFile(SINGLETON);
    }
}
