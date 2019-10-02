using System;
using System.Threading.Tasks;

namespace S64.Excite.Translator.Example
{

    class Program
    {

        private static string ApiKey
        {
            get
            {
                return Environment.GetEnvironmentVariable("EXCITE_TRANSLATOR_API_KEY");
            }
        }

        public static async Task Main(string[] args)
        {
            var client = new ExciteTranslatorClient(ApiKey);

            var result = await client.TranslateWithRetranslate("This is a pen.", source: Language.En, target: Language.Ja);

            Console.WriteLine($"{result.TranslatedText} ({result.RetranslatedText})");
        }

    }

}
