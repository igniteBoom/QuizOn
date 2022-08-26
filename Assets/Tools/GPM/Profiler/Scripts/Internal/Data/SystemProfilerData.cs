namespace Gpm.Profiler.Data
{
    using UnityEngine;

    [System.Serializable]
    public class SystemProfilerData
    {
        public bool active = true;

        public Vector2 anchoredPosition = Vector2.zero;
        public Vector2 anchorMax = Vector2.zero;
        public Vector2 anchorMin = Vector2.zero;
        public Vector2 offsetMax = Vector2.zero;
        public Vector2 offsetMin = Vector2.zero;
        public Vector2 pivot = Vector2.zero;
        public Vector2 sizeDelta = Vector2.zero;

        public ProfileInfoData os;
        public ProfileInfoData deviceModel;
        public ProfileInfoData processorType;
        public ProfileInfoData processorCount;
        public ProfileInfoData graphicDeviceName;
        public ProfileInfoData graphicDeviceVender;
        public ProfileInfoData graphicDeviceVersion;

        public void SetPostion(RectTransform trans)
        {
            anchoredPosition = trans.anchoredPosition;
            anchorMax = trans.anchorMax;
            anchorMin = trans.anchorMin;
            offsetMax = trans.offsetMax;
            offsetMin = trans.offsetMin;
            pivot = trans.pivot;
            sizeDelta = trans.sizeDelta;
        }

        public void GetPostion(RectTransform trans)
        {
            trans.anchoredPosition = anchoredPosition;
            trans.anchorMax = anchorMax;
            trans.anchorMin = anchorMin;
            trans.offsetMax = offsetMax;
            trans.offsetMin = offsetMin;
            trans.pivot = pivot;
            trans.sizeDelta = sizeDelta;
        }
    }

}