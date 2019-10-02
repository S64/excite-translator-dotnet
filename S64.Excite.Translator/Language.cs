using System;

namespace S64.Excite.Translator
{

    public enum Language
    {
        Ja,
        En,
        ZhCn,
        ZhTw,
        Ko,
        Fr,
        De,
        It,
        Es,
        Pt,
        Ru,
        Ar,
        Id,
        Uk,
        Ur,
        Et,
        Nl,
        Sv,
        Sk,
        Sl,
        Th,
        Cs,
        Tr,
        Hu,
        Hi,
        Fi,
        Bg,
        Vi,
        He,
        Fa,
        Pl,
        Lv,
        Lt,
        Ro,
    }

    public static class LanguageExt
    {

        public static string ToRequestCode(this Language language)
        {
            if (language == Language.ZhCn)
            {
                return "zh-cn";
            }
            else if (language == Language.ZhTw)
            {
                return "zh-tw";
            }
            else
            {
                return language.ToString().ToLower();
            }
        }

    }

}
