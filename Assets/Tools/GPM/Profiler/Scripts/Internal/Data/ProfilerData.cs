namespace Gpm.Profiler.Data
{
    using System.IO;

    using UnityEngine;
    using Internal;

    public class ProfilerData : ScriptableObject
    {
        public PerformanceProfilerData performance;
        public MemoryProfilerData memory;
        public RenderProfilerData render;
        public SystemProfilerData system;

        [System.NonSerialized]
        public bool isDirty = false;

        public void Save()
        {
            string json = JsonUtility.ToJson(this);

            string path = string.Format("{0}/{1}", Application.persistentDataPath, Const.SaveProfilerDataFileName);
            try
            {
                isDirty = false;
                File.WriteAllText(path, json);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
            
        }
        public void Load()
        {
            string path = string.Format("{0}/{1}", Application.persistentDataPath, Const.SaveProfilerDataFileName);

            if (File.Exists(path) == true)
            {
                try
                {
                    isDirty = false;

                    var json = File.ReadAllText(path);
                    JsonUtility.FromJsonOverwrite(json, this);
                }
                catch(System.Exception ex)
                {
                    Debug.LogException(ex);
                }
                
            }
        }

    }
}