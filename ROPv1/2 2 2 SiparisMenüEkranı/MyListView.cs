using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ROPv1
{
    public class MyListView : ListView
    {
        /// WndProc message for the left mouse button down
        /// </summary>
        const int WM_LBUTTONDOWN = 0x0201;
        const int LVM_SUBITEMHITTEST = (LVM_FIRST + 57);
        const int LVM_FIRST = 0x1000;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_LBUTTONDBLCLK = 0x0203;
        const int WM_RBUTTONDBLCLK = 0x0206;
        const int WM_RBUTTONUP = 0x0205;
        const int WM_RBUTTONDOWN = 0x0204;      
        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LVHITTESTINFO
        {
            public POINT pt;
            public LVHITTESTFLAGS flags;
            public int iItem;
            public int iSubItem;
            // Vista/Win7+
            public int iGroup;
        }

        [Flags]
        internal enum LVHITTESTFLAGS : uint
        {
            LVHT_NOWHERE = 0x00000001,
            LVHT_ONITEMICON = 0x00000002,
            LVHT_ONITEMLABEL = 0x00000004,
            LVHT_ONITEMSTATEICON = 0x00000008,
            LVHT_ONITEM = (LVHT_ONITEMICON | LVHT_ONITEMLABEL | LVHT_ONITEMSTATEICON),
            LVHT_ABOVE = 0x00000008,
            LVHT_BELOW = 0x00000010,
            LVHT_TORIGHT = 0x00000020,
            LVHT_TOLEFT = 0x00000040,
            // Vista/Win7+ only
            LVHT_EX_GROUP_HEADER = 0x10000000,
            LVHT_EX_GROUP_FOOTER = 0x20000000,
            LVHT_EX_GROUP_COLLAPSE = 0x40000000,
            LVHT_EX_GROUP_BACKGROUND = 0x80000000,
            LVHT_EX_GROUP_STATEICON = 0x01000000,
            LVHT_EX_GROUP_SUBSETLINK = 0x02000000,
        }

        private static Point LParamToPoint(IntPtr lparam)
        {
            return new Point(lparam.ToInt32() & 0xFFFF, lparam.ToInt32() >> 16);
        }

        /// call SendMessage using hit test structures
        /// </summary>
        [DllImport("User32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref LVHITTESTINFO lParam);

        protected override void WndProc(ref Message m)
        {
            //the link uses WM_LBUTTONDOWN but I found that it doesn't work
            if (m.Msg == WM_LBUTTONUP || m.Msg == WM_LBUTTONDOWN)
            {
                LVHITTESTINFO info = new LVHITTESTINFO();

                //The LParamToPOINT function I adapted to not bother with 
                //  converting to System.Drawing.Point, rather I just made 
                //  its return type the POINT struct
                Point hitPoint = LParamToPoint(m.LParam);
                info.pt.x = hitPoint.X;
                info.pt.y = hitPoint.Y;

                //if the click is on the group header, exit, otherwise send message
                if (SendMessage(this.Handle, LVM_SUBITEMHITTEST, -1, ref info) != -1)
                    if ((info.flags & LVHITTESTFLAGS.LVHT_EX_GROUP_HEADER) != 0)
                        return; //*

                if (m.Msg >= 0x201 && m.Msg <= 0x209)
                {
                    Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                    var hit = this.HitTest(pos);
                    switch (hit.Location)
                    {
                        case ListViewHitTestLocations.AboveClientArea:
                        case ListViewHitTestLocations.BelowClientArea:
                        case ListViewHitTestLocations.LeftOfClientArea:
                        case ListViewHitTestLocations.RightOfClientArea:
                        case ListViewHitTestLocations.None:
                            return;
                    }

                }
            }
            else if (m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_RBUTTONDBLCLK || m.Msg == WM_RBUTTONUP || m.Msg == WM_RBUTTONDOWN)
            {
                if (m.Msg >= 0x201 && m.Msg <= 0x209)
                {
                    Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                    var hit = this.HitTest(pos);
                    switch (hit.Location)
                    {
                        case ListViewHitTestLocations.AboveClientArea:
                        case ListViewHitTestLocations.BelowClientArea:
                        case ListViewHitTestLocations.LeftOfClientArea:
                        case ListViewHitTestLocations.RightOfClientArea:
                        case ListViewHitTestLocations.None:
                            return;
                    }

                }
            }
            base.WndProc(ref m);
        }
    }
}
