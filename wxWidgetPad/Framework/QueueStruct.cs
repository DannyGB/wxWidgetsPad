using System;
using System.Collections.Generic;
using System.Text;

namespace wxWidgetPad.Framework
{
    public class QueueStruct
    {
        public Queue<MenuStruct> Menus { get; set; }
        public string Title { get; set; }

        public QueueStruct(Queue<MenuStruct> menus, string title)
        {
            this.Menus = menus;
            this.Title = title;
        }
    }
}