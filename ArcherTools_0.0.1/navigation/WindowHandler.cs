using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;

namespace ArcherTools_0._0._1.navigation
{
    public class WindowHandler
    {
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, nint lParam);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(nint hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern nint FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(nint hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(nint hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(nint hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        internal static extern uint GetWindowLong(nint hWnd, int nIndex);

        internal const int GWL_EXSTYLE = -20;
        internal const uint WS_EX_TOPMOST = 0x00000008;
        private const int SW_RESTORE = 9;

        // Delegate for EnumWindowsProc
        public delegate bool EnumWindowsProc(nint hWnd, nint lParam);

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
            nint hWnd = FindWindow(null, windowTitle);
            if (hWnd != nint.Zero)
            {
                GetWindowRect(hWnd, out RECT rect);
                return rect.toRect();
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetWindowRectFromProcess(Process process)
        {
            nint hWnd = process.MainWindowHandle;
            if (hWnd != nint.Zero)
            {
                RECT rect;
                GetWindowRect(hWnd, out rect);
                return rect.toRect();
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetWindowRectFromHandle(nint hWnd)
        {
            if (hWnd != nint.Zero)
            {
                GetWindowRect(hWnd, out RECT rect);
                return rect.toRect();
                
            }
            return Rectangle.Empty;
        }

        public static Rectangle? GetWindowPosition(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                nint hWnd = process.MainWindowHandle;

                if (hWnd != nint.Zero)
                {
                    if (GetWindowRect(hWnd, out RECT rect))
                    {
                        return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                    }
                }
            }

            return null; // Process not found or no visible window
        }

        public static void BringProcessToFront(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    nint hWnd = process.MainWindowHandle;

                    if (hWnd != nint.Zero)
                    {
                        ShowWindow(hWnd, SW_RESTORE); // Restore if minimized
                        SetForegroundWindow(hWnd);    // Bring to front
                        Console.WriteLine($"Brought '{processName}' to foreground.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Process '{processName}' not found.");
            }
        }

        public static void WinToFocusByName(string windowName)
        {
            var hwnd = FindWindow(null, windowName);
            ShowWindow(hwnd, 1);
            SetForegroundWindow(hwnd);
            if (hwnd == nint.Zero)
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

