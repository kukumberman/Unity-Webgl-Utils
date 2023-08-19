#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace Kukumberman.WebglUtils
{
    public sealed class AntiPiracyBehaviour : MonoBehaviour
    {
#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern string GetWindowLocationAsJson();
#endif

        public UnityEvent OnValidatedSuccessfully;
        public UnityEvent OnValidationFailed;

        public bool RunOnStart;

        [TextArea(5, 10)]
        [SerializeField]
        private string _testJson;

        private AntiPiracy _antiPiracy = new();

        private void Start()
        {
#if !UNITY_WEBGL
            if (RunOnStart)
            {
                OnValidatedSuccessfully.Invoke();
            }
#else
            if (RunOnStart)
            {
                Run();
            }
#endif
        }

        public void Run()
        {
            var json = GetAppropriateJson();
            var validators = GetComponents<IWebglLocationValidator>();

            if (_antiPiracy.Validate(json, validators))
            {
                OnValidatedSuccessfully.Invoke();
            }
            else
            {
                OnValidationFailed.Invoke();
            }
        }

        private string GetAppropriateJson()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return GetWindowLocationAsJson();
#else
            return _testJson;
#endif
        }
    }
}
