using System;
using System.Collections.Generic;
using System.Text;

namespace wxWidgetPad.Framework
{
    public class MenuStruct
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsSeperator { get; set; }
        public Queue<MenuStruct> SubMenus { get; set; }

        public MenuStruct()
            : this(0, string.Empty, false)
        {
        }

        public MenuStruct(int id, string title, bool isSeperator)
        {
            this.ID = id;
            this.Title = title;
            this.IsSeperator = isSeperator;
            this.SubMenus = new Queue<MenuStruct>();
        }
    }
}