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
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(nint hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        internal const int GWL_EXSTYLE = -20;
        internal const uint WS_EX_TOPMOST = 0x00000008;

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

        public static Rectangle GetWindowPosition(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);
            if (hWnd == IntPtr.Zero)
            {
                throw new Exception("Window not found!");
            }

            if (GetWindowRect(hWnd, out RECT rect))
            {
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            else
            {
                throw new Exception("Failed to get window rectangle!");
            }
        }

        public static void WinToFocusByName(string windowName)
        {
            var hwnd = FindWindow(null, windowName);
            ShowWindow(hwnd, 1);
            SetForegroundWindow(hwnd);
            if (hwnd == IntPtr.Zero)
            {
                var processList = Process.GetProcessesByName(windowName);
                if (processList.Length > 0)
                {
                    hwnd = processList[0].MainWindowHandle;
                    ShowWindow(hwnd, 1);
                    SetForegroundWindow(hwnd);
                    Debug.WriteLine($"Bringing to front: {processList[0].MainWindowTitle}");
                }
            }
        }
    }
}

