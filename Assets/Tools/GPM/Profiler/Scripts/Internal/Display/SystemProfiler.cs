namespace Gpm.Profiler.Internal.Display
{
    using Data;
    using UnityEngine;
    using UnityEngine.UI;

    public class SystemProfiler : MonoBehaviour
    {
        public ProfileInfo os;
        public ProfileInfo deviceModel;
        public ProfileInfo processorType;
        public ProfileInfo processorCount;
        public ProfileInfo graphicDeviceName;
        public ProfileInfo graphicDeviceVender;
        public ProfileInfo graphicDeviceVersion;

        public GraphicRaycaster graphicsRayCaster;

        private SystemProfilerData data
        {
            get { return GpmProfiler.Instance.data.system; }
        }

        public void SetDragable(bool value)
        {
            graphicsRayCaster.enabled = value;
        }

        // Update is called once per frameWW
        private void Awake()
        {
            os.SetValue(SystemInfo.operatingSystem);
            deviceModel.SetValue(SystemInfo.deviceModel);
            processorType.SetValue(SystemInfo.processorType);
            processorCount.SetValue(SystemInfo.processorCount.ToString());
            graphicDeviceName.SetValue(SystemInfo.graphicsDeviceName);
            graphicDeviceVender.SetValue(SystemInfo.graphicsDeviceVendor);
            graphicDeviceVersion.SetValue(SystemInfo.graphicsDeviceVersion);
        }

        public void OnEnable()
        {
            Setting();
        }

        public void Setting()
        {
            SetSystemActive(data.active);
            SetOsActive(data.os.active);
            SetDeviceModelActive(data.deviceModel.active);
            SetProcessorTypeActive(data.processorType.active);
            SetProcessorCountActive(data.processorCount.active);
            SetGraphicDeviceNameActive(data.graphicDeviceName.active);
            SetGraphicDeviceVenderActive(data.graphicDeviceVender.active);
            SetGraphicDeviceVersionActive(data.graphicDeviceVersion.active);

            var rectTrans = transform as RectTransform;
            data.GetPostion(rectTrans);
        }

        public void SetSystemActive(bool active)
        {
            data.active = active;
            gameObject.SetActive(active);
        }

        public void SetOsActive(bool active)
        {
            data.os.active = active;
            os.gameObject.SetActive(active);
        }

        public void SetDeviceModelActive(bool active)
        {
            data.deviceModel.active = active;
            deviceModel.gameObject.SetActive(active);
        }

        public void SetProcessorTypeActive(bool active)
        {
            data.processorType.active = active;
            processorType.gameObject.SetActive(active);
        }

        public void SetProcessorCountActive(bool active)
        {
            data.processorCount.active = active;
            processorCount.gameObject.SetActive(active);
        }

        public void SetGraphicDeviceNameActive(bool active)
        {
            data.graphicDeviceName.active = active;
            graphicDeviceName.gameObject.SetActive(active);
        }

        public void SetGraphicDeviceVenderActive(bool active)
        {
            data.graphicDeviceVender.active = active;
            graphicDeviceVender.gameObject.SetActive(active);
        }

        public void SetGraphicDeviceVersionActive(bool active)
        {
            data.graphicDeviceVersion.active = active;
            graphicDeviceVersion.gameObject.SetActive(active);
        }

        public void OnMovePos()
        {
            var rectTrans = transform as RectTransform;
            data.SetPostion(rectTrans);
        }

    }
}