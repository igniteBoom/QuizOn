namespace Gpm.Profiler.Internal.Controller
{
    using Display;

    using UnityEngine;
    using UnityEngine.UI;

    public class ProfilerController : MonoBehaviour
    {
        public Toggle togglePerformance;
        public Toggle toggleMemory;
        public Toggle toggleRendering;
        public Toggle toggleSystem;

        public GameObject renderingVersionObj;
        public GameObject renderingEditObj;

        public PerformenceProfileControl performanceProfileControl;
        public MemoryProfileControl memoryProfileControl;
        public RenderingProfileControl renderingProfileControl;
        public SystemProfileControl systemProfileControl;

        public GraphicRaycaster graphicsRayCaster;

        private void Awake()
        {
#if UNITY_2020_2_OR_NEWER
            toggleRendering.interactable = true;
            renderingVersionObj.SetActive(false);
            renderingEditObj.SetActive(true);
#else
            toggleRendering.interactable = false;
            toggleRendering.SetIsOnWithoutNotify(false);

            renderingVersionObj.SetActive(true);
            renderingEditObj.SetActive(false);
#endif
        }

        private void OnEnable()
        {
            SetToggle();

            SetDragable(true);
        }

        private void OnDisable()
        {
            SetDragable(false);
        }

        public void Initialize()
        {
            SetToggle();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void SetToggle()
        {
            var data = GpmProfiler.Instance.data;

            togglePerformance.SetIsOnWithoutNotify(data.performance.active);
            toggleMemory.SetIsOnWithoutNotify(data.memory.active);
#if UNITY_2020_2_OR_NEWER
            toggleRendering.SetIsOnWithoutNotify(data.render.active);
#else
            toggleRendering.SetIsOnWithoutNotify(false);
#endif
            toggleSystem.SetIsOnWithoutNotify(data.system.active);
        }
        public void SetDragable(bool dragable)
        {
            performanceProfileControl.profiler.SetDragable(dragable);
            memoryProfileControl.profiler.SetDragable(dragable);
            renderingProfileControl.profiler.SetDragable(dragable);
            systemProfileControl.profiler.SetDragable(dragable);
        }

        public void OnClick_FrameRateEdit()
        {
            performanceProfileControl.gameObject.SetActive(true);
        }

        public void OnClick_MemoryEdit()
        {
            memoryProfileControl.gameObject.SetActive(true);
        }

        public void OnClick_RenderEdit()
        {
            renderingProfileControl.gameObject.SetActive(true);
        }

        public void OnClick_InfoEdit()
        {
            systemProfileControl.gameObject.SetActive(true);
        }

        public void OnClick_Close()
        {
            GpmProfiler.Instance.data.Save();
            gameObject.SetActive(false);
        }
    }
}
