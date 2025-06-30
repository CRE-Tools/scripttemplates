using UnityEditor;
using static PUCPR.ScriptTemplates.Editor.Static_Variables;

namespace PUCPR.ScriptTemplates.Editor
{
#if PACKAGE_ENTITIES
    public static partial class ScriptCreator
    {
        [MenuItem(MENU_PATH_ENTITIES + DOTS_BAKER, priority = 1)]
        static void CreateDOTS_BAKER() => CreateScriptFile(DOTS_BAKER);

        [MenuItem(MENU_PATH_ENTITIES + DOTS_ISYSTEM, priority = 1)]
        static void CreateDOTS_ISYSTEM() => CreateScriptFile(DOTS_ISYSTEM);

        [MenuItem(MENU_PATH_ENTITIES + DOTS_ICOMPONENTDATA, priority = 1)]
        static void CreateDOTS_ICOMPONENTDATA() => CreateScriptFile(DOTS_ICOMPONENTDATA);

        [MenuItem(MENU_PATH_ENTITIES + DOTS_IJOBENTITY, priority = 1)]
        static void CreateDOTS_IJOBENTITY() => CreateScriptFile(DOTS_IJOBENTITY);
    }
#endif
}
