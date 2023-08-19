using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    [Serializable]
    public sealed class WebglOriginValidator : IWebglLocationValidator
    {
        public List<string> AllowedOrigins = new();

        public bool Validate(JObject jsonObject)
        {
            if (jsonObject.TryGetValue("origin", out var hrefToken))
            {
                var href = hrefToken.ToObject<string>();

                if (AllowedOrigins.Contains(href))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
