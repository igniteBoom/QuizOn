namespace Gpm.Profiler.Internal.Display
{
    using UnityEngine;
    using UnityEngine.UI;
    public class ProfileInfo : MonoBehaviour
    {
        public Text textTitle;
        public Text textValue;

        public void SetValue(string value)
        {
            textValue.text = value;
        }
    }
}