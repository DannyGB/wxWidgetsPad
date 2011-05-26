using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wxWidgetPad.Models;
using wxWidgetPad.Framework.Language;
using wxWidgetPad.Framework;

namespace wxWidgetPad.Controllers
{
    public abstract class BaseController
    {
        protected WindowManager WindowManager { get; set; }
        protected LanguageManager LanguageManager { get; set; }
        protected BaseView View { get; set; }
        protected IModel Model { get; set; }

        public BaseController()
        {
            WindowManager = new WindowManager();            
            LanguageManager = new LanguageManager();
            LanguageManager.UpdateLanguage += new wxWidgetPad.Framework.Language.LanguageManager.LangaugeChangeEventHandler(LanguageManager_UpdateLanguage);
        }

        void LanguageManager_UpdateLanguage(object sender, LanguageChangeEventArgs e)
        {
            LanguageManager.ChangeLanguage(this.Model, e.Language);
        }
    }
}