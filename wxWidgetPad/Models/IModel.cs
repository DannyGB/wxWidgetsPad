using System;
using System.Collections.Generic;
using System.Text;
using wxWidgetPad.Observer;

namespace wxWidgetPad.Models
{
    public interface IModel : IObserver
    {
        void Initialise();
        void ChangeLanguage(string language);
    }
}