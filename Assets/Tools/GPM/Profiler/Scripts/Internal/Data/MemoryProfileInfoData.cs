namespace Gpm.Profiler.Data
{
    using Internal;
    using UnityEngine;

    [System.Serializable]
    public class MemoryProfileInfoData
    {
        public bool active = true;

        [System.NonSerialized]
        public long currentValue = 0;

        [System.NonSerialized]
        public long maxValue = 0;

        [System.NonSerialized]
        public long[] dataArray;

        [System.NonSerialized]
        public float[] rateArray;

        public Color color;

        public MemoryProfileInfoData()
        {
            dataArray = new long[Const.MEMORY_GRAPH_LENGTH];
            rateArray = new float[Const.MEMORY_GRAPH_LENGTH];

            for (int index = 0; index < Const.MEMORY_GRAPH_LENGTH; index++)
            {
                dataArray[index] = 0;
                rateArray[index] = 0;
            }
        }

        public void SetColor(Color value)
        {
            color = value;
        }

        public void UpdateValue(long value)
        {
            currentValue = value;
            maxValue = 0;
            for (int index = 0; index < Const.MEMORY_GRAPH_LENGTH; index++)
            {
                if (index >= Const.MEMORY_GRAPH_LENGTH - 1)
                {
                    dataArray[index] = value;
                }
                else
                {
                    dataArray[index] = dataArray[index + 1];
                }

                if (maxValue < dataArray[index])
                {
                    maxValue = dataArray[index];
                }
            }
        }
    }
}