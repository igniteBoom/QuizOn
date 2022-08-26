namespace Gpm.Profiler.Internal.Display
{
    using UnityEngine;
    using UnityEngine.UI;

    public class MemoryInfo : MonoBehaviour
    {
        public Text textTitle;
        public Text textSize;
        public Text textUnit;

        public void SetColor(Color color)
        {
            textSize.color = color;
        }
        public void Set(long size)
        {
            double kb = size / 1024.0;
            double mb = kb / 1024.0;
            double gb = mb / 1024.0;

            if (gb > 1)
            {
                textSize.text = NumberCachedString.GetTwoDecimalPlaceString((float)gb);
                textUnit.text = Const.SIZE_UNIT_GB;
            }
            else if (mb > 1)
            {
                textSize.text = NumberCachedString.GetDecimalString((float)mb);
                textUnit.text = Const.SIZE_UNIT_MB;
            }
            else if (kb > 1)
            {
                textSize.text = NumberCachedString.GetDecimalString((float)kb);
                textUnit.text = Const.SIZE_UNIT_KB;
            }
            else
            {
                textSize.text = NumberCachedString.GetIntString((int)size);
                textUnit.text = Const.SIZE_UNIT_B;
            }
        }
    }
}