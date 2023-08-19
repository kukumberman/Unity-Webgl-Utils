using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    public sealed class WebglOriginValidatorBehaviour : MonoBehaviour, IWebglLocationValidator
    {
        public WebglOriginValidator Validator = new();

        public bool Validate(JObject jsonObject)
        {
            return Validator.Validate(jsonObject);
        }
    }
}
