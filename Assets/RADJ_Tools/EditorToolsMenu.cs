using UnityEditor;
using UnityEngine;

namespace RADJ_Tools
{
    public static class EditorToolsMenu
    {
        [MenuItem("RADTools/Project/Project File Setup Tool")]
        public static void InitialiseProjectFileSetupTool()
        {
            FolderStructureSetup.InitialiseWindow();
            Debug.Log("ProjectFileSetupTool");
        }

    }
}