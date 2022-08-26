namespace Gpm.Profiler.Internal.Controller
{
    using Data;
    using Display;
    using UnityEngine;
    using UnityEngine.UI;

    public class MemoryProfileControl : MonoBehaviour
    {
        public MemoryProfiler profiler;

        public Toggle toggleMemory;

        public Toggle toggleReserved;
        public Toggle toggleAllocated;
        public Toggle toggleGfx;
        public Toggle toggleGCHeap;
        public Toggle toggleGCUsed;
        public Toggle toggleGraph;

        protected void OnEnable()
        {
            SetToggle(GpmProfiler.Instance.data.memory);
        }

        private void SetToggle(MemoryProfilerData data)
        {
            toggleMemory.SetIsOnWithoutNotify(data.active);

            toggleReserved.SetIsOnWithoutNotify(data.totalReserved.active);
            toggleAllocated.SetIsOnWithoutNotify(data.totalAllocated.active);
            toggleGfx.SetIsOnWithoutNotify(data.gfx.active);
            toggleGCHeap.SetIsOnWithoutNotify(data.gcHeap.active);
            toggleGCUsed.SetIsOnWithoutNotify(data.gcUsed.active);
            toggleGraph.SetIsOnWithoutNotify(data.activeGraph);
        }

        public void OnChangeMemory(bool active)
        {
            profiler.SetMemoryActive(active);
        }

        public void OnChangeReserved(bool active)
        {
            profiler.SetReservedActive(active);
        }

        public void OnChangeAllocated(bool active)
        {
            profiler.SetAllocatedActive(active);
        }

        public void OnChangeGfx(bool active)
        {
            profiler.SetGfxActive(active);
        }

        public void OnChangeGCHeap(bool active)
        {
            profiler.SetGCHeapActive(active);
        }

        public void OnChangeGCUsed(bool active)
        {
            profiler.SetGCUsedActive(active);
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