namespace Gpm.Profiler.Internal.Display
{

    using Data;
    using UnityEngine;
    using UnityEngine.UI;

    public class PerformanceGraph : MonoBehaviour
    {
        [SerializeField] private Image imageGraph = null;

        private PerformanceProfilerData data;

        public void Init(PerformanceProfilerData data)
        {
            this.data = data;

            imageGraph.material.SetFloatArray("_ScriptValues", data.script.rateArray);
            imageGraph.material.SetFloatArray("_RenderValues", data.render.rateArray);
            imageGraph.material.SetFloat("_GraphValues_Length", Const.PERFORMANCE_GRAPH_LENGTH);

            imageGraph.material.SetColor("_ScriptColor", data.script.color);
            imageGraph.material.SetColor("_RenderColor", data.render.color);
        }


        public void UpdateGraph()
        {
            float max = 0;
            for (int index = 0; index < Const.PERFORMANCE_GRAPH_LENGTH; index++)
            {
                if (data.script.active == true &&
                    data.render.active == false)
                {
                    float value = data.script.dataArray[index] - data.render.dataArray[index];
                    if (max < value)
                    {
                        max = value;
                    }
                }
                else
                {
                    if (data.script.active == true &&
                        max < data.script.dataArray[index])
                    {
                        max = data.script.dataArray[index];
                    }

                    if (data.render.active == true &&
                        max < data.render.dataArray[index])
                    {
                        max = data.render.dataArray[index];
                    }
                }
                    
            }

            for (int index = 0; index < Const.PERFORMANCE_GRAPH_LENGTH; index++)
            {
                data.script.rateArray[index] = data.script.active ? (float)data.script.dataArray[index] / max : 0;
                data.render.rateArray[index] = data.render.active ? (float)data.render.dataArray[index] / max : 0;

                if( data.script.active == true &&
                    data.render.active == false)
                {
                    data.script.rateArray[index] -= (float)data.render.dataArray[index] / max;
                }
            }

            imageGraph.material.SetFloat("_GraphValues_Length", 64);
            imageGraph.material.SetFloatArray("_ScriptValues", data.script.rateArray);
            imageGraph.material.SetFloatArray("_RenderValues", data.render.rateArray);
        }
    }
}
