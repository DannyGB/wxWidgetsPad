using System;
using System.Collections.Generic;
using wx;
using wxWidgetPad.Controllers;

namespace wxWidgetPad
{
    public class App : wx.App
    {
        public override bool OnInit()
        {
            Form1 frame = new Form1();
            frame.Show(true);

            return true;
        }

        [STAThread]
        static void Main()
        {
            App app = new App();
            app.Run();
        }
    }
}