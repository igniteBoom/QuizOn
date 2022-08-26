namespace Gpm.Profiler.Internal
{
#if UNITY_2019_1_OR_NEWER
#else
    using UnityEngine.UI;
    using System.Reflection;

    public static class ToggleSetIsOnWithoutNotifyExtensions
    {
        private static MethodInfo toggleSetIsOnWithoutNotifyMethod;

        static ToggleSetIsOnWithoutNotifyExtensions()
        {
            MethodInfo[] methods = typeof(Toggle).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            for (var index = 0; index < methods.Length; index++)
            {
                if (methods[index].Name == "Set" && methods[index].GetParameters().Length == 2)
                {
                    toggleSetIsOnWithoutNotifyMethod = methods[index];
                    break;
                }
            }
        }
        public static void SetIsOnWithoutNotify(this Toggle instance, bool value)
        {
            toggleSetIsOnWithoutNotifyMethod.Invoke(instance, new object[] { value, false });
        }
    }
#endif

}