namespace Gpm.Profiler.Internal.Display
{
    using Data;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Profiling;

    public class MemoryProfiler : MonoBehaviour
    {
        private float deltaTime = 0.25f;
        public float updateTime = 0.25f;

        public MemoryInfo textReserved;
        public MemoryInfo textAllocated;
        public MemoryInfo textGfx;
        public MemoryInfo textGCHeap;
        public MemoryInfo textGCUsed;

        public MemoryGraph memoryGraph;

        public GraphicRaycaster graphicsRayCaster;

        private MemoryProfilerData data
        {
            get { return GpmProfiler.Instance.data.memory; }
        }

        public void SetMemoryActive(bool active)
        {
            data.active = active;

            gameObject.SetActive(active);
        }

        public void SetReservedActive(bool active)
        {
            data.totalReserved.active = active;

            textReserved.gameObject.SetActive(active);
        }

        public void SetAllocatedActive(bool active)
        {
            data.totalAllocated.active = active;

            textAllocated.gameObject.SetActive(active);
        }

        public void SetGfxActive(bool active)
        {
            if(Debug.isDebugBuild == false)
            {
                active = false;
            }
            data.gfx.active = active;

            textGfx.gameObject.SetActive(active);
        }

        public void SetGCHeapActive(bool active)
        {
            data.gcHeap.active = active;

            textGCHeap.gameObject.SetActive(active);
        }

        public void SetGCUsedActive(bool active)
        {
            data.gcUsed.active = active;

            textGCUsed.gameObject.SetActive(active);
        }

        public void SetGraphActive(bool active)
        {
            data.activeGraph = active;

            memoryGraph.gameObject.SetActive(active);
        }


        public void Setting()
        {
            memoryGraph.Init();

            SetMemoryActive(data.active);
            SetReservedActive(data.totalReserved.active);
            SetAllocatedActive(data.totalAllocated.active);
            SetGfxActive(data.gfx.active);
            SetGCHeapActive(data.gcHeap.active);
            SetGCUsedActive(data.gcUsed.active);
            SetGraphActive(data.activeGraph);

            textReserved.SetColor(data.totalReserved.color);
            textAllocated.SetColor(data.totalAllocated.color);
            textGfx.SetColor(data.gfx.color);
            textGCHeap.SetColor(data.gcHeap.color);
            textGCUsed.SetColor(data.gcUsed.color);

            var rectTrans = transform as RectTransform;
            data.GetPostion(rectTrans);
        }

        public void SetDragable(bool value)
        {
            graphicsRayCaster.enabled = value;
        }

        private void OnEnable()
        {
            Setting();
        }

        // Update is called once per frame
        void Update()
        {
            deltaTime += Time.deltaTime;

            if (deltaTime > updateTime)
            {
                long reservedMemory = Profiler.GetTotalReservedMemoryLong();
                long allocatedMemory = Profiler.GetTotalAllocatedMemoryLong();
                long managedHeapMemory = Profiler.GetMonoHeapSizeLong();
                long managedUsedMemory = Profiler.GetMonoUsedSizeLong();
                long graphicsDriverMemory = Profiler.GetAllocatedMemoryForGraphicsDriver();

                textReserved.Set(reservedMemory);
                textAllocated.Set(allocatedMemory);
                textGfx.Set(graphicsDriverMemory);
                textGCHeap.Set(managedHeapMemory);
                textGCUsed.Set(managedUsedMemory);

                data.totalReserved.UpdateValue(reservedMemory);
                data.totalAllocated.UpdateValue(allocatedMemory);
                data.gfx.UpdateValue(graphicsDriverMemory);
                data.gcHeap.UpdateValue(managedHeapMemory);
                data.gcUsed.UpdateValue(managedUsedMemory);

                if(data.activeGraph == true)
                {
                    memoryGraph.UpdateGraph();
                }

                deltaTime = 0;
            }
        }

        public void OnMovePos()
        {
            var rectTrans = transform as RectTransform;
            data.SetPostion(rectTrans);
        }
    }
}