namespace Gpm.Profiler.Internal.Display
{
    using UnityEngine;
    using UnityEngine.UI;
    using Unity.Profiling;

    public class RenderInfo : MonoBehaviour
    {
        public Text textTitle;

        public Text textValue;

        public string statistic;
#if UNITY_2020_2_OR_NEWER
        private ProfilerRecorder recorder;

        void OnEnable()
        {
            recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, statistic);
        }

        void OnDisable()
        {
            recorder.Dispose();
        }

        public void UpdateValue()
        {
            if (recorder.Valid == true)
            {
                textValue.text = NumberCachedString.GetIntString((int)recorder.LastValue);
            }
            else
            {
                textValue.text = "Not Vaild";
            }
        }
    
#else
        public void OnEnable()
        {
            textValue.text = "Not Support Version";
        }

        public void UpdateValue()
        {
        }

#endif

    }
}