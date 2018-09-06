using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace SuspensionApp {
    public partial class Form1 : Form {

        public Form1() {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;

            InitializeComponent();

            button1.Click += ButtonClick;
        }

        protected override void OnLoad(EventArgs e) {
            this.TopMost = true;
            var side = 25;
            this.Width = 48 + side;
            this.Height = 23 + side;
            this.Top = 155 - 19;
            this.Left = 304 - 19;
            //this.Top = Screen.PrimaryScreen.Bounds.Height - Height - 60;
        }

        //protected override void OnPaint(PaintEventArgs e) {
        //    //Graphics g = e.Graphics;
        //    //Color FColor = Color.Red;
        //    //Color TColor = Color.Yellow;
        //    //Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.ForwardDiagonal);
        //    //g.FillRectangle(b, this.ClientRectangle);
        //}

        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 0x0001;
        const int HTCAPTION = 0x0002;
        protected override void WndProc(ref System.Windows.Forms.Message m) {
            switch (m.Msg) {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if (m.Result == (IntPtr)HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        void ButtonClick(object sender, EventArgs ev) {
            try {
                CloseAllChromeBrowsers();
            } catch (Exception e) {
                System.Console.Out.WriteLine(e);
            }
            System.Environment.Exit(0);
        }

        static void CloseAllChromeBrowsers() {
            foreach (Process process in Process.GetProcessesByName("chrome")) {
                if (process.MainWindowHandle == IntPtr.Zero) // some have no UI
                    continue;
                AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                if (element != null) {
                    ((WindowPattern)element.GetCurrentPattern(WindowPattern.Pattern)).Close();
                }
            }
        }
    }
}
