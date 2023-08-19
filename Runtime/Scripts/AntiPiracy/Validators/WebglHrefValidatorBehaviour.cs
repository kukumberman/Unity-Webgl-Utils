using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    public sealed class WebglHrefValidatorBehaviour : MonoBehaviour, IWebglLocationValidator
    {
        public WebglHrefValidator Validator = new();

        public bool Validate(JObject jsonObject)
        {
            return Validator.Validate(jsonObject);
        }
    }
}
