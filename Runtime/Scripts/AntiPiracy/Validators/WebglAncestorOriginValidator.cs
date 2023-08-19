using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    [Serializable]
    public sealed class WebglAncestorOriginValidator : IWebglLocationValidator
    {
        public int Index;
        public List<string> AllowedOrigins = new();

        public bool Validate(JObject jsonObject)
        {
            if (!jsonObject.TryGetValue("ancestorOrigins", out var ancestorOriginsToken))
            {
                // https://developer.mozilla.org/en-US/docs/Web/API/Location/ancestorOrigins
                // not supported in Firefox
                // allow user to play in this case
                return true;
            }

            var ancestorOrigins = ancestorOriginsToken.ToObject<JObject>();

            var indexAsKey = Index.ToString();

            if (ancestorOrigins.TryGetValue(indexAsKey, out var originToken))
            {
                var origin = originToken.ToObject<string>();

                if (AllowedOrigins.Contains(origin))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
