using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.methods
{
    internal class Receiving
    {
        public static void MainCall()
        {
            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(nint hWnd);

            if (Process.GetProcessesByName("Notepad").Count() <= 0)
            {
                Process.Start("Notepad.exe");
            }
            else
            {
                IntPtr hWnd = Process.GetProcessesByName("Notepad")[0].MainWindowHandle;
                if (hWnd != IntPtr.Zero)
                {
                    SetForegroundWindow(hWnd);
                    Thread.Sleep(500);
                }
            }
            if (ConfigData._receivingConfig != null)
            {
                ReceivingConfig config = ConfigData._receivingConfig;
                MousePosition MousePos1 = config.getMousePosByType(ControlType.ReceiptLineFirstLine);
                MousePosition MousePos2 = config.getMousePosByType(ControlType.ItemSearchInputBox);

                if (MousePos1 != null)
                {
                    List<int> mouseCoords1 = MousePos1.getPosition();
                    MouseHandler.SetCursorPos(mouseCoords1[0], mouseCoords1[1]);
                    if (MousePos2 != null)
                    {
                        List<int> mouseCoords2 = MousePos2.getPosition();
                        MouseHandler.RelativeMouseDrag(new Point(mouseCoords1[0],mouseCoords1[1]), new Point(mouseCoords2[0],mouseCoords2[1]), true);
                    }
                }
            }
            
        }
    }
}
