using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils
{
    public interface IWebglLocationValidator
    {
        bool Validate(JObject jsonObject);
    }
}
