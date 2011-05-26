using System;
using System.Drawing;
using wxWidgetPad.Models;
using wxWidgetPad.Observer;
using wx;
using wxWidgetPad.Framework;
using wxWidgetPad.Resources;

namespace wxWidgetPad.Controllers
{
    public class Form1 : BaseController, IObserver
    {
        public Form1()
            : base()
        {
        }

        public void OnCloseMenuClick(object o, Event e)
        {
            ((Views.Form1)this.View).text.Value = string.Empty;
            ((Models.Form1)Model).CloseCurrentFileStream();
        }

        public void OnCopyMenuClick(object o, Event e)
        {
            if (((Views.Form1)this.View).text.CanCopy())
            {
                ((Views.Form1)this.View).text.Copy();
            }
        }

        public void OnCutMenuClick(object o, Event e)
        {
            if (((Views.Form1)this.View).text.CanCut())
            {
                ((Views.Form1)this.View).text.Cut();
            }
        }

        public void OnDeleteMenuClick(object o, Event e)
        {
        }

        public void OnPasteMenuClick(object o, Event e)
        {
            if (((Views.Form1)this.View).text.CanPaste())
            {
                ((Views.Form1)this.View).text.Paste();
            }
        }

        public void OnSaveMenuClick(object o, Event e)
        {
            if (string.IsNullOrEmpty(((Models.Form1)Model).Filename))
            {
                this.OnSaveAsMenuClick(o, e);
                
                return;
            }

            ((Models.Form1)Model).SaveFileStream(((Views.Form1)this.View).text.Value);
        }

        public void OnSaveAsMenuClick(object o, Event e)
        {
            FileDialog fd = new FileDialog(this.View, strings.SaveFile);
            fd.WindowStyle = WindowStyles.FD_SAVE;
            if (ShowModalResult.OK == fd.ShowModal())
            {
                ((Models.Form1)Model).SaveFileStream(string.Format(@"{0}\{1}", fd.Directory, fd.Filename), ((Views.Form1)this.View).text.Value, false);
            }
        }

        public void OnOpenMenuClick(object o, Event e)
        {
            ((Models.Form1)Model).CloseCurrentFileStream();
            FileDialog fd = new FileDialog(this.View, strings.OpenFile);
            if (ShowModalResult.OK == fd.ShowModal())
            {
                ((Models.Form1)Model).GetFileStream(string.Format(@"{0}\{1}", fd.Directory, fd.Filename));
            }
        }

        public void OnLanguageChangeClick(object o, Event e)
        {
            CommandEvent ev = (CommandEvent)e;
            switch (ev.ID)
            {
                case (int)Views.Form1.ID_CONTROLS.ID_MENU_GERMAN_LANGUAGE:
                    LanguageManager.ThrowUpdateLanguage(Constants.Instance.Langauges[(int)Constants.LanguageCode.German]);
                    break;
                case (int)Views.Form1.ID_CONTROLS.ID_MENU_ENGLISH_LANGUAGE:
                    LanguageManager.ThrowUpdateLanguage(Constants.Instance.Langauges[(int)Constants.LanguageCode.English]);
                    break;
            }
        }

        public void OnExitMenuClick(object o, Event e)
        {
            this.WindowManager.Views.Remove(this.View);
            this.View.Close();
        }

        public void OnAboutMenuClick(object o, Event e)
        {
            MessageDialog.ShowModal(strings.Copyright, strings.Title, WindowStyles.DIALOG_OK | WindowStyles.ICON_INFORMATION);
        }

        public bool Show(Point position, Size size, bool show)
        {
            // Create the view
            this.View = new wxWidgetPad.Views.Form1(position, size, this);
            WindowManager.Views.Add(this.View);

            // link the view to the controller (observer pattern)
            this.Notify += new wxWidgetPad.Observer.Observer.ObserverEventHandler(this.View.WireUpEvents);
            this.ThrowNotify();

            // Create the model
            this.Model = new wxWidgetPad.Models.Form1();

            // Link the view to the model (observer pattern)
            this.Model.Notify += new wxWidgetPad.Observer.Observer.ObserverEventHandler(this.View.ReRender);

            // initialise the model (this will cause the view to update too)
            this.Model.Initialise();

            // Show the view
            return this.View.Show(show);
        }

        public bool Show(bool show)
        {
            return this.Show(Window.wxDefaultPosition, Window.wxDefaultSize, show);
        }

        public void ThrowNotify()
        {
            if (this.Notify != null)
            {
                this.Notify(this, new ObserverEventArgs());
            }
        }

        #region IObserver Members

        public event wxWidgetPad.Observer.Observer.ObserverEventHandler Notify;

        #endregion

    }
}