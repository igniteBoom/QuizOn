namespace Gpm.Profiler.Internal.Display
{
    using Data;
    using UnityEngine;
    using UnityEngine.UI;

    public class MemoryGraph : MonoBehaviour
    {
        [SerializeField] private Image imageGraph = null;

        private MemoryProfilerData data;

        public void Init()
        {
            this.data = GpmProfiler.Instance.data.memory;

            imageGraph.material.SetFloatArray("_TotalReservedValues", data.totalReserved.rateArray);
            imageGraph.material.SetFloatArray("_TotalAllocatedValues", data.totalAllocated.rateArray);
            imageGraph.material.SetFloatArray("_GfxValues", data.gfx.rateArray);
            imageGraph.material.SetFloatArray("_GCHeapValues", data.gcHeap.rateArray);
            imageGraph.material.SetFloatArray("_GCUsedValues", data.gcUsed.rateArray);
            imageGraph.material.SetFloat("_GraphValues_Length", Const.MEMORY_GRAPH_LENGTH);

            imageGraph.material.SetColor("_TotalReservedColor", data.totalReserved.color);
            imageGraph.material.SetColor("_TotalAllocatedColor", data.totalAllocated.color);
            imageGraph.material.SetColor("_GfxColor", data.gfx.color);
            imageGraph.material.SetColor("_GCHeapColor", data.gcHeap.color);
            imageGraph.material.SetColor("_GCUsedColor", data.gcUsed.color);
        }


        public void UpdateGraph()
        {
            long currentMaxMemory = 0;
            if (data.totalReserved.active &&
                currentMaxMemory < data.totalReserved.maxValue)
            {
                currentMaxMemory = data.totalReserved.maxValue;
            }

            if (data.totalAllocated.active &&
                currentMaxMemory < data.totalAllocated.maxValue)
            {
                currentMaxMemory = data.totalAllocated.maxValue;
            }

            if (data.gfx.active &&
                currentMaxMemory < data.gfx.maxValue)
            {
                currentMaxMemory = data.gfx.maxValue;
            }

            if (data.gcHeap.active &&
                currentMaxMemory < data.gcHeap.maxValue)
            {
                currentMaxMemory = data.gcHeap.maxValue;
            }
            
            if (data.gcUsed.active &&
                currentMaxMemory < data.gcUsed.maxValue)
            {
                currentMaxMemory = data.gcUsed.maxValue;
            }
            
            for (int index = 0; index < Const.MEMORY_GRAPH_LENGTH; index++)
            {
                data.totalReserved.rateArray[index] = data.totalReserved.active ? (float)data.totalReserved.dataArray[index] / currentMaxMemory : 0;
                data.totalAllocated.rateArray[index] = data.totalAllocated.active ? (float)data.totalAllocated.dataArray[index] / currentMaxMemory : 0;

                long managedReserved = data.gcHeap.dataArray[index];
                if (data.gfx.active &&
                    data.gfx.dataArray[index] > 0)
                {
                    long gfx = data.gfx.dataArray[index];

                    if (data.gcHeap.active)
                    {
                        gfx += managedReserved;
                        managedReserved -= data.gfx.dataArray[index];
                    }

                    data.gfx.rateArray[index] = data.gfx.active ? (float)gfx / currentMaxMemory : 0;
                }
                else
                {
                    data.gfx.rateArray[index] = 0;
                }

                data.gcHeap.rateArray[index] = data.gcHeap.active ? (float)managedReserved / currentMaxMemory : 0;
                data.gcUsed.rateArray[index] = data.gcUsed.active ? (float)data.gcUsed.dataArray[index] / currentMaxMemory : 0;
            }

            imageGraph.material.SetFloat("_GraphValues_Length", Const.MEMORY_GRAPH_LENGTH);
            imageGraph.material.SetFloatArray("_TotalReservedValues", data.totalReserved.rateArray);
            imageGraph.material.SetFloatArray("_TotalAllocatedValues", data.totalAllocated.rateArray);
            imageGraph.material.SetFloatArray("_GfxValues", data.gfx.rateArray);
            imageGraph.material.SetFloatArray("_GCHeapValues", data.gcHeap.rateArray);
            imageGraph.material.SetFloatArray("_GCUsedValues", data.gcUsed.rateArray);
        }
    }
}
