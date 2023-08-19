using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    public sealed class WebglAncestorOriginValidatorBehaviour
        : MonoBehaviour,
            IWebglLocationValidator
    {
        public WebglAncestorOriginValidator Validator;

        public bool Validate(JObject jsonObject)
        {
            return Validator.Validate(jsonObject);
        }
    }
}
