using System;

namespace S64.Excite.Translator
{

    public class Request
    {
        
        public string Q { get; set; }
        public Language Source { get; set; }
        public Language Target { get; set; }
        public bool ReverseOption { get; set; }

    }

}
