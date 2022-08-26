namespace Gpm.Profiler.Internal.Controller
{
    using Display;
    using UnityEngine;

    public class SystemProfileControl : MonoBehaviour
    {
        public SystemProfiler profiler;

        public void OnChangeSystem(bool active)
        {
            profiler.SetSystemActive(active);
        }

        public void OnChangeOs(bool active)
        {
            profiler.SetOsActive(active);
        }

        public void OnChangeDeviceModel(bool active)
        {
            profiler.SetDeviceModelActive(active);
        }

        public void OnChangeProcessorType(bool active)
        {
            profiler.SetProcessorTypeActive(active);
        }

        public void OnChangeProcessorCount(bool active)
        {
            profiler.SetProcessorCountActive(active);
        }

        public void OnChangeGraphicDeviceName(bool active)
        {
            profiler.SetGraphicDeviceNameActive(active);
        }

        public void OnChangeGraphicDeviceVender(bool active)
        {
            profiler.SetGraphicDeviceVenderActive(active);
        }

        public void OnChangeGraphicDeviceVersion(bool active)
        {
            profiler.SetGraphicDeviceVersionActive(active);
        }

        public void OnClick_Close()
        {
            gameObject.SetActive(false);
        }
    }
}