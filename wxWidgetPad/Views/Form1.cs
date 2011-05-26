using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using wx;
using wxWidgetPad.Framework;
using wxWidgetPad.Observer;

namespace wxWidgetPad.Views
{
    public class Form1 : BaseView
    {
        public enum ID_CONTROLS
        {            
            ID_MENU_OPEN,
            ID_MENU_EXIT,
            ID_MENU_ABOUT,
            ID_FORM_TITLE,
            ID_MENU_BAR,
            ID_PANEL,
            ID_MENU_GERMAN_LANGUAGE,
            ID_MENU_ENGLISH_LANGUAGE,
            ID_MENU_LANGUAGES,
            ID_TEXT,
            ID_MENU_EDIT,
            ID_MENU_COPY,
            ID_MENU_CUT,
            ID_MENU_PASTE,
            ID_MENU_DELETE,
            ID_MENU_SAVE,
            ID_MENU_SAVE_AS,
            ID_MENU_NEW,
            ID_MENU_CLOSE,
        }

        ScrolledWindow panel;
        public wx.TextCtrl text;

        public Form1(Point pos, Size size, Controllers.Form1 controller)
            : base("Form1", pos, size)
        {
            this.Closing += new EventListener(Form1_Closing);

            panel = new ScrolledWindow(this, (int)ID_CONTROLS.ID_PANEL /*, wxDefaultPosition, wxDefaultSize, WindowStyles.TAB_TRAVERSAL | WindowStyles.CLIP_CHILDREN | WindowStyles.BORDER_NONE*/);
            panel.AutoLayout = true;

            // At this point we don't know about the Model (so we don't have any labels etc..) so we have to wait for the model to get in touch
            text = new TextCtrl(panel, (int)ID_CONTROLS.ID_TEXT, string.Empty, wxDefaultPosition, wxDefaultSize, wx.WindowStyles.TE_MULTILINE | wx.WindowStyles.BORDER_SUNKEN);
        }        

        void Form1_Closing(object sender, Event e)
        {
            this.Destroy();
        }

        private wx.Menu CreateMenu(Queue<MenuStruct> menus)
        {
            Menu parentMenu = new Menu();

            foreach (MenuStruct menu in menus)
            {
                if (menu.IsSeperator)
                {
                    parentMenu.AppendSeparator();
                }
                else
                {
                    MenuItem menuItem = new MenuItem(parentMenu, menu.ID, menu.Title);

                    if (menu.SubMenus != null && menu.SubMenus.Count > 0)
                    {
                        menuItem.SubMenu = this.CreateMenu(menu.SubMenus);
                    }

                    parentMenu.Append(menuItem);
                }
            }

            return parentMenu;
        }

        /// <summary>
        /// Handle the Notify event of the <see cref="IObserver"/> interface for the Model
        /// </summary>
        /// <param name="model">The <see cref="Models.Form1"/></param>
        /// <param name="e">The <see cref="System.EventArgs"/></param>
        public override void ReRender(object model, ObserverEventArgs e)
        {
            Models.Form1 __model = (Models.Form1)model;

            foreach (int changedControl in e.ChangedControls)
            {
                switch (changedControl)
                {
                    case (int)ID_CONTROLS.ID_TEXT:
                        this.text.Clear();
                        this.text.AppendText(__model.File.ReadToEnd());
                        break;
                    case (int)ID_CONTROLS.ID_FORM_TITLE:
                        this.Title = __model.Title;
                        break;
                    case (int)ID_CONTROLS.ID_MENU_BAR:
                        this.MenuBar = new MenuBar();
                        foreach (QueueStruct list in __model.Menus)
                        {
                            MenuBar.Append(this.CreateMenu(list.Menus), list.Title);
                        }
                        break;
                    default:
                        break;
                }
            }            
        }

        /// <summary>
        /// Handle the Notify event of the <see cref="IObserver"/> interface for the Controller
        /// </summary>
        /// <param name="controller">The <see cref="Controllers.Form1"/></param>
        /// <param name="e">The <see cref="System.EventAgs"/></param>
        public override void WireUpEvents(object controller, ObserverEventArgs e)
        {
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_ABOUT, new EventListener(((Controllers.Form1)controller).OnAboutMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_EXIT, new EventListener(((Controllers.Form1)controller).OnExitMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_OPEN, new EventListener(((Controllers.Form1)controller).OnOpenMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_GERMAN_LANGUAGE, new EventListener(((Controllers.Form1)controller).OnLanguageChangeClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_ENGLISH_LANGUAGE, new EventListener(((Controllers.Form1)controller).OnLanguageChangeClick));

            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_COPY, new EventListener(((Controllers.Form1)controller).OnCopyMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_CUT, new EventListener(((Controllers.Form1)controller).OnCutMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_DELETE, new EventListener(((Controllers.Form1)controller).OnDeleteMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_PASTE, new EventListener(((Controllers.Form1)controller).OnPasteMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_SAVE, new EventListener(((Controllers.Form1)controller).OnSaveMenuClick));
            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_SAVE_AS, new EventListener(((Controllers.Form1)controller).OnSaveAsMenuClick));

            EVT_MENU((int)Views.Form1.ID_CONTROLS.ID_MENU_CLOSE, new EventListener(((Controllers.Form1)controller).OnCloseMenuClick));            

            EVT_SIZE(new EventListener(this.OnSize));
        }

        public void OnSize(object sender, Event e)
        {
            DoSize();
            e.Skip();
        }

        public void DoSize()
        {
            if (this.text == null) return;

            Size size = ClientSize;
            int y = size.Height;
            text.SetSize(0, 0, size.Width, y);
        }
    }
}