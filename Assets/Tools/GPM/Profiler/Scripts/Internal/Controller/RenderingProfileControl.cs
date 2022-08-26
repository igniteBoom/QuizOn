namespace Gpm.Profiler.Internal.Controller
{
    using Data;
    using Display;
    using UnityEngine;
    using UnityEngine.UI;

    public class RenderingProfileControl : MonoBehaviour
    {
        public RenderProfiler profiler;

        public Toggle toggleRendering;

        public Toggle toggleSetPass;
        public Toggle toggleDrawcalls;
        public Toggle toggleTotalBatch;
        public Toggle toggleTriangles;
        public Toggle toggleVertices;

        protected void OnEnable()
        {
            SetToggle(GpmProfiler.Instance.data.render);
        }

        private void SetToggle(RenderProfilerData data)
        {
            toggleRendering.SetIsOnWithoutNotify(data.active);

            toggleSetPass.SetIsOnWithoutNotify(data.setPass.active);
            toggleDrawcalls.SetIsOnWithoutNotify(data.drawcalls.active);
            toggleTotalBatch.SetIsOnWithoutNotify(data.totalBatch.active);
            toggleTriangles.SetIsOnWithoutNotify(data.triangles.active);
            toggleVertices.SetIsOnWithoutNotify(data.vetices.active);
        }

        public void OnChangeRendering(bool active)
        {
            profiler.SetRenderingActive(active);
        }

        public void OnChangeSetPass(bool active)
        {
            profiler.SetSetPassActive(active);
        }

        public void OnChangeDrawcalls(bool active)
        {
            profiler.SetDrawCallsActive(active);
        }

        public void OnChangeTotalBatch(bool active)
        {
            profiler.SetTotalBatchActive(active);
        }

        public void OnChangeTriangles(bool active)
        {
            profiler.SetTrianglesActive(active);
        }

        public void OnChangeVertices(bool active)
        {
            profiler.SetVerrticesActive(active);
        }

        public void OnClick_Close()
        {
            gameObject.SetActive(false);
        }
    }
}