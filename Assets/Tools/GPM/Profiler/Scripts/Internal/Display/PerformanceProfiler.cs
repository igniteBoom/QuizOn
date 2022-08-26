namespace Gpm.Profiler.Internal.Display
{
    using Data;

    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Diagnostics;

    public class PerformanceProfiler : MonoBehaviour
    {
        private float deltaTime = 0f;
        private float renderTime = 0f;

        private int frameCount = 0;

        public float updateTime = 0.25f;

        public GameObject fpsObject;
        public GameObject avgGroupObject;
        public GameObject scriptObject;
        public GameObject renderObject;

        public Text fpsText;
        public Text cpuText;
        public Text avgText;
        public Text minText;
        public Text maxText;
        public Text scriptText;
        public Text renderText;

        public PerformanceGraph perfomanceGraph;

        public GraphicRaycaster graphicsRayCaster;

        private PerformanceProfilerData data
        {
            get { return GpmProfiler.Instance.data.performance; }
        }

        private bool checkRenderTime = true;
        private readonly Stopwatch renderTimeStopWatch = Stopwatch.StartNew();

        public void SetPerformanceActive(bool active)
        {
            data.active = active;

            gameObject.SetActive(active);
        }

        public void SetFPSActive(bool active)
        {
            data.fps.active = active;

            fpsObject.SetActive(active);
        }

        public void SetAvgGroupActive(bool active)
        {
            data.avgGroup.active = active;

            avgGroupObject.SetActive(active);
        }

        public void SetScriptActive(bool active)
        {
            data.script.active = active;

            scriptObject.SetActive(active);
        }

        public void SetRenderActive(bool active)
        {
            data.render.active = active;

            renderObject.SetActive(active);
        }

        public void SetGraphActive(bool active)
        {
            data.activeGraph = active;

            perfomanceGraph.gameObject.SetActive(active);
        }

        public void Setting()
        {
            SetPerformanceActive(data.active);
            SetFPSActive(data.fps.active);
            SetAvgGroupActive(data.avgGroup.active);
            SetScriptActive(data.script.active);
            SetRenderActive(data.render.active);
            SetGraphActive(data.activeGraph);

            scriptText.color = data.script.color;
            renderText.color = data.render.color;

            var rectTrans = transform as RectTransform;
            data.GetPostion(rectTrans);
        }

        public void SetDragable(bool value)
        {
            graphicsRayCaster.enabled = value;
        }

        private void Awake()
        {
            data.Init();
            perfomanceGraph.Init(data);
        }

       
        private void OnEnable()
        {
            Setting();

            data.Reset();

            frameCount = 0;
            deltaTime = 0;
            renderTime = 0;
            renderTimeStopWatch.Stop();
            renderTimeStopWatch.Reset();

            checkRenderTime = true;
            StartCoroutine(CheckRenderTime());
        }

        private void OnDisable()
        {
            checkRenderTime = false;
            StopCoroutine(CheckRenderTime());
        }

        // Update is called once per frame
        private void Update()
        {
            deltaTime += Time.deltaTime;

            frameCount++;

            if (deltaTime > updateTime)
            {
                float fps = frameCount / deltaTime;
                float msec = deltaTime / frameCount * 1000f;

                float render_msec = renderTime / frameCount;

                data.script.UpdateValue(msec);
                data.render.UpdateValue(render_msec);

                perfomanceGraph.UpdateGraph();

                deltaTime = 0f;
                renderTime = 0f;
                frameCount = 0;

                fpsText.text = NumberCachedString.GetIntString(Mathf.RoundToInt(fps));
                cpuText.text = NumberCachedString.GetDecimalString(msec);
                avgText.text = NumberCachedString.GetDecimalString(data.script.avgValue);
                minText.text = NumberCachedString.GetDecimalString(data.script.minValue);
                maxText.text = NumberCachedString.GetDecimalString(data.script.maxValue);

                scriptText.text = NumberCachedString.GetDecimalString(msec - render_msec);
                renderText.text = NumberCachedString.GetDecimalString(render_msec);
            }
        }


        private void LateUpdate()
        {
            if (checkRenderTime == true)
            {
                renderTimeStopWatch.Start();
            }
        }

        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

        private IEnumerator CheckRenderTime()
        {
            while (checkRenderTime == true)
            {
                yield return WaitForEndOfFrame;

                renderTimeStopWatch.Stop();
                renderTime += (float)renderTimeStopWatch.Elapsed.TotalMilliseconds;
                renderTimeStopWatch.Reset();
            }
        }

        public void OnMovePos()
        {
            var rectTrans = transform as RectTransform;
            data.SetPostion(rectTrans);
        }
    }
}