using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;

namespace ArcherTools_0._0._1
{
    public class WindowHandler
    {
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Delegate for EnumWindowsProc
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public Rectangle toRect()
            {
                return new Rectangle(Left, Top, Right - Left, Bottom - Top);
            }
        }

        public static Rectangle GetWindowRectFromName(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);
            if (hWnd != IntPtr.Zero)
            {
                GetWindowRect(hWnd, out RECT rect);
                return rect.toRect();
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetWindowRectFromProcess(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            if (hWnd != IntPtr.Zero)
            {
                RECT rect;
                GetWindowRect(hWnd, out rect);
                return rect.toRect();
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetWindowRectFromHandle(IntPtr hWnd)
        {
            if (hWnd != IntPtr.Zero)
            {                
                GetWindowRect(hWnd, out RECT rect);
                return rect.toRect();
            }
            return Rectangle.Empty;
        }
    }
}
