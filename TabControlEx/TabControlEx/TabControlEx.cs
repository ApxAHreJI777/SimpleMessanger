using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ClosableTabControl
{
    public class ClosableTabPage : TabPage
    {
        bool highlighted = false;
        public bool Highlighted 
        { 
            get { return highlighted; }
            set { highlighted = value;  }
        }
        public ClosableTabPage()
            : base()
        {
            highlighted = false;
        }

        public ClosableTabPage(string text)
            : base(text)
        {
            highlighted = false;
        }
    }

    public delegate bool PreRemoveTab(int indx);
    public class TabControlEx : TabControl
    {
        public TabControlEx()
            : base()
        {
            PreRemoveTabPage = null;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        public PreRemoveTab PreRemoveTabPage;
        public event TabClosedEventHendler TabClosed;
        int closeButtonW = 8;
        int closeButtonH = 8;
        Color closeButtonXColor = Color.White;
        Color closeButtonColor = Color.Red;
        SolidBrush activTabBrush = new SolidBrush(Color.White);
        Color disTabDefColor1 = Color.White;
        Color disTabDefColor2 = Color.LightGray;
        Color disTabHlColor1 = Color.Yellow;
        Color disTabHlColor2 = Color.Orange;
        Pen disTabBorderPen = new Pen(Color.Gray);
        LinearGradientBrush disTabBrush;

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle r;
            Rectangle rx;
            Brush b = new SolidBrush(Color.Black);
            Brush buttonBrush = new SolidBrush(closeButtonColor);
            Color disTabColor1;
            Color disTabColor2;
            Pen p = new Pen(closeButtonXColor, 2);
            string titel;
            Font f = this.Font;

            for (int i = 0; i < TabCount; i++)
            {
                disTabColor1 = disTabDefColor1;
                disTabColor2 = disTabDefColor2;
                ClosableTabPage tb = TabPages[i] as ClosableTabPage;
                if (tb != null)
                {
                    if (tb.Highlighted)
                    {
                        disTabColor1 = disTabHlColor1;
                        disTabColor2 = disTabHlColor2;
                    }
                }
                r = GetTabRect(i);

                if (i == this.SelectedIndex)
                    e.Graphics.FillRectangle(activTabBrush, r);
                else
                {
                    disTabBrush = new LinearGradientBrush(r, disTabColor1, disTabColor2, LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(disTabBrush, r);
                    GraphicsPath bPath = new GraphicsPath();
                    int c = 5;
                    bPath.AddLine(r.X, r.Y + r.Height - 2, r.X, r.Y + c);
                    bPath.AddArc(r.X, r.Y, c, c, 180, 90);
                    bPath.AddLine(r.X + c, r.Y, r.X + r.Width - c, r.Y);
                    bPath.AddArc(r.X + r.Width - c, r.Y, c, c, 270, 90);
                    bPath.AddLine(r.X + r.Width, r.Y + c, r.X + r.Width, r.Y + r.Height - 2);
                    e.Graphics.DrawPath(disTabBorderPen, bPath);
                }

                r.Offset(2, 2);
                r.Width = closeButtonW;
                r.Height = closeButtonH;
                rx = new Rectangle(r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
                titel = this.TabPages[i].Text;
                
                e.Graphics.FillRectangle(buttonBrush, r);
                e.Graphics.DrawLine(p, rx.X, rx.Y, rx.X + rx.Width, rx.Y + rx.Height);
                e.Graphics.DrawLine(p, rx.X + rx.Width, rx.Y, rx.X, rx.Y + rx.Height);

                e.Graphics.DrawString(titel, f, b, new PointF(r.X + r.Width, r.Y));
            }
            

        }

        public void HighlightTab(int index)
        {
            ClosableTabPage tb = TabPages[index] as ClosableTabPage;
            if (tb != null)
            {
                tb.Highlighted = true;
            }
        }

        public void HighlightTab(string key)
        {
            int i = TabPages.IndexOfKey(key);
            if(i >= 0)
                HighlightTab(i);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            MouseButtons btn = e.Button;
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle r = GetTabRect(i);
                if(r.Contains(p))
                {
                    if (btn == MouseButtons.Middle)
                    {
                        CloseTab(i);
                    }
                    else
                    {
                        r.Offset(2, 2);
                        r.Width = closeButtonW;
                        r.Height = closeButtonH;
                        if (r.Contains(p))
                        {
                            CloseTab(i);
                        }
                    }
                }
            }
        }

        private void CloseTab(int i)
        {
            if (PreRemoveTabPage != null)
            {
                bool closeIt = PreRemoveTabPage(i);
                if (!closeIt)
                    return;
            }
            TabPage page = TabPages[i];
            TabPages.Remove(page);
            if (TabPages.Count > i)
                SelectTab(i);
            else
            {
                i--;
                if (i >= 0 && TabPages.Count > i)
                    SelectTab(i);
            }
            OnTabeClosed(new TabClosedEventArgs(page));
        }

        protected virtual void OnTabeClosed(TabClosedEventArgs e)
        {
            TabClosedEventHendler handler = TabClosed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
    public delegate void TabClosedEventHendler(object sender, TabClosedEventArgs e);
    public class TabClosedEventArgs : EventArgs
    {
        public TabClosedEventArgs(TabPage page)
        {
            p = page;
        }
        private TabPage p;
        public TabPage Page
        {
            get { return p; }
        }
    }
}
