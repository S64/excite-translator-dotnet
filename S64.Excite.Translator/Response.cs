using System;

namespace S64.Excite.Translator
{

    public interface Response
    {
        string TranslatedText { get; }
    }

    public interface RetranslatedResponse : Response
    {
        string RetranslatedText { get; }
    }

}
