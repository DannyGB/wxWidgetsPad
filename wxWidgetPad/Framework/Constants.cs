using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxWidgetPad.Framework
{
    internal class Constants
    {
        public enum LanguageCode
        {
            English = 0,
            German
        }

        private static Constants constants;
        public List<System.Collections.Generic.KeyValuePair<int, string>> Langauges { get; set; }

        private Constants()
        {
            Langauges = new List<KeyValuePair<int, string>>(2);
            Langauges.Add(new KeyValuePair<int, string>(0, "en"));
            Langauges.Add(new KeyValuePair<int, string>(1, "de-DE"));
        }

        public static Constants Instance
        {
            get
            {
                if (constants == null)
                {
                    constants = new Constants();
                }

                return constants;
            }
        }
    }
}