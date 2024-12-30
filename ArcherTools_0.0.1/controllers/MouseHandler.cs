using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.controllers
{
    internal class MouseHandler
    {
        [DllImport("user32.dll")]
        internal static extern bool SetCursorPos(int X, int Y);

        internal static void SetCursorPosRelative(int X, int Y)
        {
            Point curMousePos = Control.MousePosition;
            X += curMousePos.X;
            Y += curMousePos.Y;
            SetCursorPos(X, Y);            
        }

        internal static void MouseClick()
        {
            const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
            const uint MOUSEEVENTF_LEFTUP = 0x0004;

            [DllImport("user32.dll", SetLastError = true)]
            static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
        }
    }
}
