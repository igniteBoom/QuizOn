

namespace Gpm.AssetManagement.AssetFind
{
    public class PrefabStageRoot
    {
        public PrefabStageRoot(UnityEditor.SceneManagement.PrefabStage prefab)
        {
            this.prefab = prefab;
#if UNITY_2020_1_OR_NEWER
            this.path = prefab.assetPath;
#else
            this.path = prefab.prefabAssetPath;
#endif
            this.handle = prefab.scene.handle;

            PrefabStageRootManager.changePrefabRoot += ChangePrefabRoot;
        }

        public void ReOpen(UnityEditor.SceneManagement.PrefabStage prefab)
        {
            this.prefab = prefab;
            this.handle = prefab.scene.handle;
        }

        public void Remove()
        {
            removed = true;
        }

        public UnityEditor.SceneManagement.PrefabStage prefab;
        public string path;
        public int handle;
        private bool removed = false;

        private void ChangePrefabRoot(PrefabStageRoot addPrefabRoot, PrefabStageRoot removePrefabRoot)
        {
            if (removed == true)
            {
                if (addPrefabRoot != null)
                {
                    if (path.Equals(addPrefabRoot.path) == true)
                    {
                        ReOpen(addPrefabRoot.prefab);
                        removed = false;
                    }
                }
            }
        }
    }
}