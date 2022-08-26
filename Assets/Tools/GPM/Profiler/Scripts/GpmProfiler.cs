namespace Gpm.Profiler
{
    using UnityEngine;

    using Data;
    using Internal;
    using Internal.Controller;

    public class GpmProfiler : MonoBehaviour
    {
        public const string VERSION = "1.0.2";

        [Header("Gesture")]
        [Space]
        public bool gestureEnable = true;

        [Space(5)]
        
        public ProfilerData defaultData;

        [System.NonSerialized]
        public ProfilerData data;

        public ProfilerController controller;
        
        private static GpmProfiler instance = null;

        public static GpmProfiler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GpmProfiler>();

                    if (instance == null)
                    {
                        instance = new GameObject("GpmProfiler").AddComponent<GpmProfiler>();
                    }
                    instance.LoadData();
                }

                return instance;
            }
        }

        public void SetDefaultData()
        {
            data = Instantiate(instance.defaultData);
            data.isDirty = true;
        }

        private void LoadData()
        {
            data = Instantiate(instance.defaultData);
            data.Load();
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                instance.LoadData();
            }

            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        
        public void Show()
        {
            controller.Show();
        }
        
        
        private void Initialize()
        {
            controller.Initialize();
        }

        private void Update()
        {
            if (gestureEnable == true)
            {
                CheckGesture();
                CheckKey();
            }
        }

        private bool isTouchBegin = false;
        private float touchTime = 0f;

        private void CheckGesture()
        {
            if (Input.touchCount == Const.GESTURE_TOUCH_COUNT)
            {
                if (isTouchBegin == false)
                {
                    isTouchBegin = true;
                    touchTime = Time.unscaledTime;
                }
                else
                {
                    if (Time.unscaledTime - touchTime >= Const.GESTURE_TOUCH_TIME_INTERVAL)
                    {
                        isTouchBegin = false;
                        touchTime = 0;
                        Show();
                    }
                }
            }
            else
            {
                isTouchBegin = false;
                touchTime = 0;
            }
        }

        private void CheckKey()
        {
            if (Input.GetKeyDown(KeyCode.F5) == true)
            {
                Show();
            }
        }        
    }
}
