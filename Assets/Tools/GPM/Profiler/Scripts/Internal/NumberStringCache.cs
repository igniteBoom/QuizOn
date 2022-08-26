namespace Gpm.Profiler.Internal
{

    using System.Collections.Generic;
    using UnityEngine;

    static public class NumberCachedString
    {
        static private Dictionary<int, string> intCachedString = new Dictionary<int, string>();
        static private Dictionary<float, string> floatCachedString = new Dictionary<float, string>();

        static public string GetDecimalString(float value)
        {
            float msec = Mathf.Floor(value * 10) / 10;

            string str = null;
            if (floatCachedString.TryGetValue(msec, out str) == true)
            {
                return str;
            }
            else
            {
                str = msec.ToString("0.0");
                floatCachedString.Add(msec, str);
            }

            return str;
        }

        static public string GetTwoDecimalPlaceString(float value)
        {
            float msec = Mathf.Floor(value * 100) / 100;

            string str = null;
            if (floatCachedString.TryGetValue(msec, out str) == true)
            {
                return str;
            }
            else
            {
                str = msec.ToString("0.00");
                floatCachedString.Add(msec, str);
            }

            return str;
        }

        static public string GetIntString(int value)
        {
            string str = null;
            if (intCachedString.TryGetValue(value, out str) == true)
            {
                return str;
            }
            else
            {
                str = value.ToString();
                intCachedString.Add(value, str);
            }

            return str;
        }
    }
}