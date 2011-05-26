using System;
using System.Collections.Generic;
using System.Text;

namespace wxWidgetPad.Observer
{
    public abstract class Observer
    {
        public delegate void ObserverEventHandler(object model, ObserverEventArgs e);
    }
}