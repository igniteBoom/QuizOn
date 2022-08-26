namespace Gpm.Profiler.Internal.Display
{
    using Data;
    using UnityEngine;
    using UnityEngine.UI;

    public class RenderProfiler : MonoBehaviour
    {
#if UNITY_2020_2_OR_NEWER
        private float deltaTime = 0.25f;
#endif
        public float updateTime = 0.25f;

        public RenderInfo textSetPass;
        public RenderInfo textDrawcalls;
        public RenderInfo textTotalBatch;
        public RenderInfo textTriangles;
        public RenderInfo textVetices;

        public GraphicRaycaster graphicsRayCaster;

        private RenderProfilerData data
        {
            get { return GpmProfiler.Instance.data.render; }
        }

        public void SetDragable(bool value)
        {
            graphicsRayCaster.enabled = value;
        }

#if UNITY_2020_2_OR_NEWER
        public void OnEnable()
        {
            Setting();
        }

        void Update()
        {
            deltaTime += Time.deltaTime;

            if (deltaTime > updateTime)
            {
                textSetPass.UpdateValue();
                textDrawcalls.UpdateValue();
                textTotalBatch.UpdateValue();
                textTriangles.UpdateValue();
                textVetices.UpdateValue();
        
                deltaTime = 0;
            }
        }
#else
        public void OnEnable()
        {
            gameObject.SetActive(false);
        }
#endif

        public void Setting()
        {
            SetRenderingActive(data.active);
            SetSetPassActive(data.setPass.active);
            SetDrawCallsActive(data.drawcalls.active);
            SetTotalBatchActive(data.totalBatch.active);
            SetTrianglesActive(data.triangles.active);
            SetVerrticesActive(data.vetices.active);

            var rectTrans = transform as RectTransform;
            data.GetPostion(rectTrans);
        }

        public void SetRenderingActive(bool active)
        {
            data.active = active;
            gameObject.SetActive(active);
        }

        public void SetSetPassActive(bool active)
        {
            data.setPass.active = active;
            textSetPass.gameObject.SetActive(active);
        }

        public void SetDrawCallsActive(bool active)
        {
            data.drawcalls.active = active;
            textDrawcalls.gameObject.SetActive(active);
        }

        public void SetTotalBatchActive(bool active)
        {
            data.totalBatch.active = active;
            textTotalBatch.gameObject.SetActive(active);
        }

        public void SetTrianglesActive(bool active)
        {
            data.triangles.active = active;
            textTotalBatch.gameObject.SetActive(active);
        }

        public void SetVerrticesActive(bool active)
        {
            data.vetices.active = active;
            textVetices.gameObject.SetActive(active);
        }

        public void OnMovePos()
        {
            var rectTrans = transform as RectTransform;
            data.SetPostion(rectTrans);
        }
    }
}