using System.Runtime.InteropServices;

namespace WindowsShadeSimple
{
    public partial class FormShade : Form
    {
        public bool Sizeable = true;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 0x10;
        private const int HTBOTTOMRIGHT = 17;

        private bool m_aeroEnabled = false;
        private int _x;
        private int _y;

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        public FormShade()
        {
            InitializeComponent();
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                case 0x0084:
                    if (Sizeable)
                    {
                        Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                        {
                            if (vPoint.Y <= 5)
                            {
                                m.Result = (IntPtr)HTTOPLEFT;
                            }
                            else
                            if (vPoint.Y >= ClientSize.Height - 5)
                            {
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            }
                            else
                            {
                                m.Result = (IntPtr)HTLEFT;
                            }
                        }
                        else
                        if (vPoint.X >= ClientSize.Width - 5)
                        {
                            if (vPoint.Y <= 5)
                            {
                                m.Result = (IntPtr)HTTOPRIGHT;
                            }
                            else
                            if (vPoint.Y >= ClientSize.Height - 5)
                            {
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                            }
                            else
                            {
                                m.Result = (IntPtr)HTRIGHT;
                            }
                        }
                        else
                        if (vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)HTTOP;
                        }
                        else
                        {
                            if (vPoint.Y >= ClientSize.Height - 5)
                            {
                                m.Result = (IntPtr)HTBOTTOM;
                            }
                        }

                    }
                    break;

                default:
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.Escape == keyData)
            {
                Environment.Exit(0);
            }
            else if (Keys.Delete == keyData)
            {
                this.Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FormShade_MouseDown(object sender, MouseEventArgs e)
        {
            this._x = e.X;
            this._y = e.Y;
        }
        private void FormShade_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(this.Location.X + e.X - this._x, this.Location.Y + e.Y - this._y);
        }

        private void FormShade_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            new FormShade().Show();
        }
    }
}
