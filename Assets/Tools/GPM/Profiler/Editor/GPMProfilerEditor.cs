namespace Gpm.Profiler.Editor
{
    using Data;
    using UnityEditor;
    using UnityEngine;

    public class GpmProfilerEditor : EditorWindow
    {
        ProfilerData data;

        [MenuItem("Tools/GPM/Profiler/Window")]
        public  static void Open()
        {
            // Get existing open window or if none, make a new one:
            GpmProfilerEditor window = (GpmProfilerEditor)EditorWindow.GetWindow(typeof(GpmProfilerEditor), true, "GPMProfilerEditor");
            window.Init();
            window.Show();
        }

        public void Init()
        {
            data = AssetDatabase.LoadAssetAtPath<ProfilerData>("Assets/GPM/Profiler/Prefabs/DefaultProfilerData.asset");
        }

        protected void OnGUI()
        {
            GUILayout.Label("Default Setting");
            if (data != null)
            {
                var edit = Editor.CreateEditor(data);

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    edit.DrawDefaultInspector();
                    if (check.changed == true )
                    {
                        AssetDatabase.SaveAssets();
                    }
                }
                
            }

            
        }
    }
}