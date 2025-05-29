using UnityEngine;

namespace PUCPR.ScriptTemplates
{
    //[CreateAssetMenu(fileName = "Settings_" + nameof(ScriptTemplates), menuName = "Tool Settings/Script Templates")]
    public class SO_ToolSettings : ScriptableObject
    {
        [SerializeField] public bool showdebugs = true;
    }
}
