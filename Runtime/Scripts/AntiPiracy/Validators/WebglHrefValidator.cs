using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    [Serializable]
    public sealed class WebglHrefValidator : IWebglLocationValidator
    {
        public List<string> AllowedHrefs = new();

        public bool Validate(JObject jsonObject)
        {
            if (jsonObject.TryGetValue("href", out var hrefToken))
            {
                var href = hrefToken.ToObject<string>();

                if (AllowedHrefs.Contains(href))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
