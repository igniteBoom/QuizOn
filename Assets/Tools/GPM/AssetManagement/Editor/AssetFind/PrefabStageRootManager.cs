using UnityEditor;
using UnityEngine;


namespace Gpm.AssetManagement.AssetFind
{
    static public class PrefabStageRootManager
    {
        private static PrefabStageRoot activePrefabStage = null;

        public delegate void PrefabRootChangeCallback(PrefabStageRoot addPrefabRoot, PrefabStageRoot removePrefabRoot);
        public static event PrefabRootChangeCallback changePrefabRoot;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                activePrefabStage = new PrefabStageRoot(prefabStage);
            }

            UnityEditor.SceneManagement.PrefabStage.prefabStageOpened += prefabStageOpened;
            UnityEditor.SceneManagement.PrefabStage.prefabStageClosing += prefabStageClosing;
            UnityEditor.SceneManagement.PrefabStage.prefabSaved += prefabSaved;
        }

        public static PrefabStageRoot GetCurrentPrefabStageRoot()
        {
            return activePrefabStage;
        }

        private static void prefabStageOpened(UnityEditor.SceneManagement.PrefabStage prefabStage)
        {
            if (activePrefabStage != null)
            {
                if (changePrefabRoot != null)
                {
                    changePrefabRoot(null, activePrefabStage);
                }
                activePrefabStage = null;
            }

            activePrefabStage = new PrefabStageRoot(prefabStage);

            if (changePrefabRoot != null)
            {
                changePrefabRoot(activePrefabStage, null);
            }
        }

        private static void prefabStageClosing(UnityEditor.SceneManagement.PrefabStage prefabStage)
        {
            if (changePrefabRoot != null)
            {
                changePrefabRoot(null, activePrefabStage);
            }

            activePrefabStage = null;
        }

        private static void prefabSaved(GameObject prefab)
        {
        }
    }
}