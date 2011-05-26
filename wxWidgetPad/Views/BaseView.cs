using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using wxWidgetPad.Observer;

namespace wxWidgetPad.Framework
{
    public abstract class BaseView : wx.Frame
    {
        public BaseView()
            : this(string.Empty, new Point(), new Size())
        {
        }

        public BaseView(string title, Point pos, Size size)
            : base(title, pos, size)
        {
        }

        public abstract void WireUpEvents(object model, ObserverEventArgs e);

        public abstract void ReRender(object model, ObserverEventArgs e);
    }
}