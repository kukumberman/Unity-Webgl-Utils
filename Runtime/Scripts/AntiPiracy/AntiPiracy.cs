using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Kukumberman.WebglUtils
{
    public sealed class AntiPiracy
    {
        public bool Validate(string json, IEnumerable<IWebglLocationValidator> validators)
        {
            var jsonObject = JObject.Parse(json);

            foreach (var validator in validators)
            {
                if (validator.Validate(jsonObject))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
