using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace S64.Excite.Translator
{

    public class ExciteTranslatorClient
    {

        private readonly Uri ENDPOINT = new Uri("https://api-world.excite.co.jp/translate/");

        private readonly string apiKey;
        private readonly HttpClient client;

        public ExciteTranslatorClient(string apiKey)
        {
            this.apiKey = apiKey;

            var handler = new HttpClientHandler
            {
                UseCookies = false
            };

            client = new HttpClient(handler);
        }

        public async Task<Response> Translate(Request request)
        {
            var query = HttpUtility.ParseQueryString("");
            query.Add("q", request.Q);
            query.Add("apikey", apiKey);
            query.Add("source", request.Source.ToRequestCode());
            query.Add("target", request.Target.ToRequestCode());
            query.Add("reverse_option", request.ReverseOption ? "1" : "0");
            query.Add("format", "");

            var uri = new UriBuilder(ENDPOINT)
            {
                Query = query.ToString()
            }.Uri;

            var res = await client.GetAsync(uri);
            var content = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new NotImplementedException(content);
            }

            var obj = JObject.Parse(content);

            var translatedText = obj.SelectToken("$.data.translations.translatedText").ToString();

            if (!request.ReverseOption)
            {
                return new ResponseObj
                {
                    TranslatedText = translatedText
                };
            }
            else
            {
                return new RetranslatedResponseObj
                {
                    TranslatedText = translatedText,
                    RetranslatedText = obj.SelectToken("$.data.retranslations.translatedText").ToString()
                };
            }
        }

        public async Task<Response> Translate(string query, Language source, Language target, bool withRetranslate = false)
        {
            return await Translate(
                new Request
                {
                    Q = query,
                    Source = source,
                    Target = target,
                    ReverseOption = withRetranslate
                }
            );
        }

        public async Task<RetranslatedResponse> TranslateWithRetranslate(string query, Language source, Language target)
        {
            return (await Translate(query, source: source, target: target, withRetranslate: true)) as RetranslatedResponse;
        }

    }

    internal class ResponseObj : S64.Excite.Translator.Response
    {
        public string TranslatedText { get; set; }
    }

    internal class RetranslatedResponseObj : ResponseObj, S64.Excite.Translator.RetranslatedResponse
    {
        //public string TranslatedText { get; set; }
        public string RetranslatedText { get; set; }
    }

}
