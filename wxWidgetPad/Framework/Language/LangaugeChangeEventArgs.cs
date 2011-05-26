using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxWidgetPad.Framework.Language
{
    public class LanguageChangeEventArgs
    {
        public KeyValuePair<int, string> Language { get; set; }

        public LanguageChangeEventArgs()
        {
        }

        public LanguageChangeEventArgs(KeyValuePair<int, string> language)
        {
            this.Language = language;
        }
    }
}