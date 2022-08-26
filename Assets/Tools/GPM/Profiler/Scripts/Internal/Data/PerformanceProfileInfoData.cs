namespace Gpm.Profiler.Data
{
    using Internal;
    using UnityEngine;

    [System.Serializable]
    public class PerformanceProfileInfoData
    {
        public bool active = true;

        [System.NonSerialized]
        public float currentValue = 0;

        [System.NonSerialized]
        public float avgValue = 0;

        [System.NonSerialized]
        public float minValue = 0;

        [System.NonSerialized]
        public float maxValue = 0;

        [System.NonSerialized]
        public float[] dataArray;

        [System.NonSerialized]
        public float[] rateArray;

        public Color color;

        public void Init()
        {
            dataArray = new float[Const.PERFORMANCE_GRAPH_LENGTH];
            rateArray = new float[Const.PERFORMANCE_GRAPH_LENGTH];

            Reset();
        }

        public void Reset()
        {
            for (int index = 0; index < Const.PERFORMANCE_GRAPH_LENGTH; index++)
            {
                dataArray[index] = 0;
                rateArray[index] = 0;
            }
        }

        public void SetColor(Color value)
        {
            color = value;
        }

        public void UpdateValue(float value)
        {
            currentValue = value;

            minValue = float.MaxValue;
            maxValue = 0;

            int valueCount = 0;
            float sumValue = 0;
            for (int index = 0; index < Const.PERFORMANCE_GRAPH_LENGTH; index++)
            {
                if (index >= Const.PERFORMANCE_GRAPH_LENGTH - 1)
                {
                    dataArray[index] = value;
                }
                else
                {
                    dataArray[index] = dataArray[index + 1];
                }

                if (dataArray[index] > 0)
                {
                    if (minValue > dataArray[index])
                    {
                        minValue = dataArray[index];
                    }

                    sumValue += dataArray[index];

                    valueCount++;
                }
                if (maxValue < dataArray[index])
                {
                    maxValue = dataArray[index];
                }
            }

            if (valueCount > 0)
            {
                avgValue = sumValue / valueCount;
            }
        }
    }
}