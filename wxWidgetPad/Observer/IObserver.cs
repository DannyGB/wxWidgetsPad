using System;
using System.Collections.Generic;
using System.Text;

namespace wxWidgetPad.Observer
{
    public interface IObserver
    {
        event wxWidgetPad.Observer.Observer.ObserverEventHandler Notify;
    }
}