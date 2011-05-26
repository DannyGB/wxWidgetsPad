using System;
using System.Collections.Generic;

#if VERSION_3_POINT_5
using System.Linq;
#endif

using System.Text;
using wxWidgetPad.Observer;
using wxWidgetPad.Framework;
using System.Configuration;
using System.Reflection;
using wxWidgetPad.Resources;
using System.Globalization;
using System.Threading;
using System.IO;
using wx;

namespace wxWidgetPad.Models
{
    public class Form1 : IModel
    {
        public string Filename
        {
            get;
            set;
        }

        private StreamReader file;
        public StreamReader File
        {
            get
            {
                return file;
            }
            set
            {
                this.file = value;
                ThrowNotify((int)Views.Form1.ID_CONTROLS.ID_TEXT);
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                this.title = value;
                ThrowNotify((int)Views.Form1.ID_CONTROLS.ID_FORM_TITLE);
            }
        }

        private Queue<QueueStruct> menus = new Queue<QueueStruct>(0);
        public Queue<QueueStruct> Menus
        {
            get
            {
                return this.menus;
            }

            set
            {
                this.menus = value;
                ThrowNotify((int)Views.Form1.ID_CONTROLS.ID_MENU_BAR);
            }
        }
        
        public Form1()
        {
        }
            
        private void ThrowNotify(int controlId)
        {
            if (this.Notify != null)
            {
                this.Notify(this, new ObserverEventArgs(new Queue<int>(new int[] { controlId })));
            }
        }

        #region IModel Members

        public void Initialise()
        {
            this.ChangeLanguage("en");
        }

        public void GetFileStream(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                this.Filename = filename;
                this.File = new StreamReader(System.IO.File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read));
            }
        }

        public void ChangeLanguage(string language)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);

            this.Title = strings.Title;
            this.Menus = new Queue<QueueStruct>(3);

            Queue<QueueStruct> menus = new Queue<QueueStruct>(3);

            Queue<MenuStruct> list = new Queue<MenuStruct>();
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_NEW, strings.New, false));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_OPEN, strings.Open, false));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_CLOSE, strings.Close, false));
            list.Enqueue(new MenuStruct(0, string.Empty, true));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_SAVE, strings.Save, false));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_SAVE_AS, strings.SaveAs, false));
            list.Enqueue(new MenuStruct(0, string.Empty, true));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_EXIT, strings.Exit, false));

            QueueStruct queue = new QueueStruct(list, strings.File);
            menus.Enqueue(queue);

            list = new Queue<MenuStruct>();
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_CUT, strings.Cut, false));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_COPY, strings.Copy, false));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_PASTE, strings.Paste, false));
            list.Enqueue(new MenuStruct(0, string.Empty, true));
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_DELETE, strings.Delete, false));

            queue = new QueueStruct(list, strings.Edit);
            menus.Enqueue(queue);

            list = new Queue<MenuStruct>();
            list.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_ABOUT, strings.About, false));
            list.Enqueue(new MenuStruct(0, string.Empty, true));

            MenuStruct menuLangs = new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_LANGUAGES, strings.Languages, false);
            menuLangs.SubMenus.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_GERMAN_LANGUAGE, strings.GermanLanguage, false));
            menuLangs.SubMenus.Enqueue(new MenuStruct((int)Views.Form1.ID_CONTROLS.ID_MENU_ENGLISH_LANGUAGE, strings.EnglishLanguage, false));

            list.Enqueue(menuLangs);

            queue = new QueueStruct(list, strings.Help);

            menus.Enqueue(queue);

            this.Menus = menus;
        }

        #endregion

        #region IObserver Members

        public event wxWidgetPad.Observer.Observer.ObserverEventHandler Notify;

        #endregion

        internal void SaveFileStream(string filename, string stream, bool overwrite)
        {
            if (!System.IO.File.Exists(filename))
            {
                if (this.File == null || !filename.Equals(this.Filename))
                {
                    FileStream str = new FileStream(filename, FileMode.CreateNew);
                    this.file = new StreamReader(str);
                }

                using (StreamWriter sw = new StreamWriter(this.File.BaseStream))
                {
                    sw.Write(stream);
                }
            }
            else
            {
                if (overwrite || ShowModalResult.YES == MessageDialog.ShowModal(strings.Overwrite, strings.Title, WindowStyles.DIALOG_YES_NO | WindowStyles.ICON_QUESTION))
                {
                    this.CloseCurrentFileStream();
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.Write(stream);
                    }
                }
            }
        }

        internal void CloseCurrentFileStream()
        {
            if (this.File == null)
            {
                return;
            }

            this.File.Close();
            this.File.Dispose();
        }

        internal void SaveFileStream(string stream)
        {
            this.SaveFileStream(this.Filename, stream, true);
        }
    }
}