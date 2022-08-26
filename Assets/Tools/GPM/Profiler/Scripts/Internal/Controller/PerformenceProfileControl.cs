namespace Gpm.Profiler.Internal.Controller
{
    using Data;
    using Display;
    using UnityEngine;
    using UnityEngine.UI;

    public class PerformenceProfileControl : MonoBehaviour
    {
        public PerformanceProfiler profiler;

        public Toggle togglePerformance;

        public Toggle toggleFps;
        public Toggle toggleAvgGroup;
        public Toggle toggleScript;
        public Toggle toggleRender;
        public Toggle toggleGraph;

        protected void OnEnable()
        {

            SetToggle(GpmProfiler.Instance.data.performance);
        }

        private void SetToggle(PerformanceProfilerData data)
        {
            togglePerformance.SetIsOnWithoutNotify(data.active);

            toggleFps.SetIsOnWithoutNotify(data.fps.active);
            toggleAvgGroup.SetIsOnWithoutNotify(data.avgGroup.active);
            toggleScript.SetIsOnWithoutNotify(data.script.active);
            toggleRender.SetIsOnWithoutNotify(data.render.active);
            toggleGraph.SetIsOnWithoutNotify(data.activeGraph);
        }

        public void OnChangePerformanceActive(bool active)
        {
            profiler.SetPerformanceActive(active);
        }

        public void OnChangeFps(bool active)
        {
            profiler.SetFPSActive(active);
        }

        public void OnChangeAvgGroup(bool active)
        {
            profiler.SetAvgGroupActive(active);
        }

        public void OnChangeScript(bool active)
        {
            profiler.SetScriptActive(active);
        }

        public void OnChangeRender(bool active)
        {
            profiler.SetRenderActive(active);
        }

        public void OnChangeGraph(bool active)
        {
            profiler.SetGraphActive(active);
        }

        public void OnClick_Close()
        {
            gameObject.SetActive(false);
        }
    }
}