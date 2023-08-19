using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Kukumberman.WebglUtils.Tests
{
    public sealed class OriginValidatorTest
    {
        [Test]
        public void Succeeds()
        {
            var definedOrigin = "http://127.0.0.1:5500";
            var userOrigin = definedOrigin;

            var result = ValidateOrigin(definedOrigin, userOrigin);
            Assert.IsTrue(result);
        }

        [Test]
        public void Fails()
        {
            var definedOrigin = "http://127.0.0.1:5500";
            var userOrigin = "http://127.0.0.1:5501";

            var result = ValidateOrigin(definedOrigin, userOrigin);
            Assert.IsFalse(result);
        }

        private static bool ValidateOrigin(string definedOrigin, string userOrigin)
        {
            var originValidator = new WebglOriginValidator();
            originValidator.AllowedOrigins.Add(definedOrigin);

            var antiPiracy = new AntiPiracy();
            var json = CreateValidJsonWithOrigin(userOrigin);
            return antiPiracy.Validate(json, new[] { originValidator });
        }

        private static string CreateValidJsonWithOrigin(string origin)
        {
            var o = new JObject();
            o["origin"] = origin;
            return o.ToString();
        }
    }

    public sealed class HrefValidatorTest
    {
        [Test]
        public void Succeeds()
        {
            var definedHref = "http://127.0.0.1:5500/index.html";
            var userHref = definedHref;

            var result = ValidateHref(definedHref, userHref);
            Assert.IsTrue(result);
        }

        [Test]
        public void Fails()
        {
            var definedHref = "http://127.0.0.1:5500/index.html";
            var userHref = "http://127.0.0.1:5501/index.html";

            var result = ValidateHref(definedHref, userHref);
            Assert.IsFalse(result);
        }

        private static bool ValidateHref(string definedHref, string userHref)
        {
            var hrefValidator = new WebglHrefValidator();
            hrefValidator.AllowedHrefs.Add(definedHref);

            var antiPiracy = new AntiPiracy();
            var json = CreateValidJsonWithHref(userHref);
            return antiPiracy.Validate(json, new[] { hrefValidator });
        }

        private static string CreateValidJsonWithHref(string href)
        {
            var o = new JObject();
            o["href"] = href;
            return o.ToString();
        }
    }

    public sealed class AncestorOriginValidatorTest
    {
        [Test]
        public void Succeeds()
        {
            var definedOrigin = "http://127.0.0.1:5500/";
            var userOrigin = definedOrigin;

            var result = ValidateAncestorOrigin(definedOrigin, userOrigin);
            Assert.IsTrue(result);
        }

        [Test]
        public void Fails()
        {
            var definedOrigin = "http://127.0.0.1:5500/";
            var userOrigin = "http://127.0.0.1:5501/";

            var result = ValidateAncestorOrigin(definedOrigin, userOrigin);
            Assert.IsFalse(result);
        }

        private static bool ValidateAncestorOrigin(string definedOrigin, string userOrigin)
        {
            var validator = new WebglAncestorOriginValidator();
            validator.Index = 0;
            validator.AllowedOrigins.Add(definedOrigin);

            var antiPiracy = new AntiPiracy();
            var json = CreateValidJsonWithAncestorOrigin(validator.Index, userOrigin);
            return antiPiracy.Validate(json, new[] { validator });
        }

        private static string CreateValidJsonWithAncestorOrigin(int index, string origin)
        {
            return $"{{\"ancestorOrigins\": {{\"{index}\": \"{origin}\" }} }}";
        }
    }
}
