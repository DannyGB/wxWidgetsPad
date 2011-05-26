using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wxWidgetPad.Observer;
using wxWidgetPad.Controllers;
using wxWidgetPad.Models;

namespace wxWidgetPad.Framework
{
    public class WindowManager
    {
        public List<BaseView> Views { get; set; }

        public WindowManager()
        {
            this.Views = new List<BaseView>();
        }
    }
}