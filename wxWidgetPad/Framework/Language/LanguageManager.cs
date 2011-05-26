using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wxWidgetPad.Models;
using wxWidgetPad.Controllers;

namespace wxWidgetPad.Framework.Language
{
    public class LanguageManager
    {
        public delegate void LangaugeChangeEventHandler(object o, LanguageChangeEventArgs e);
        public event LangaugeChangeEventHandler UpdateLanguage;

        public void ChangeLanguage(IModel model, KeyValuePair<int, string> language)
        {
            model.ChangeLanguage(language.Value);
        }    

        public void ThrowUpdateLanguage(KeyValuePair<int, string> language)
        {
            if (this.UpdateLanguage != null)
            {
                this.UpdateLanguage(this, new LanguageChangeEventArgs(language));
            }
        }
    }
}