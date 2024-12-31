using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.controllers
{
    internal class VPNHandler
    {
        internal void ConnectToVPN()
        {
            // Sys Methods

            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            // VPN Information

            String vpnPath = "C:\\Program Files\\SonicWall\\Global VPN Client";
            String vpnShortcutName = "SWGVC.exe";
            String vpnShortcutPath = Path.Combine(vpnPath, vpnShortcutName);

            try
            {
                Process[] processes = Process.GetProcessesByName(vpnShortcutName);
                if (processes.Length <= 0)
                {
                    Process.Start(vpnShortcutPath);
                    Thread.Sleep(500);
                    processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(vpnShortcutName));

                }
                IntPtr hWnd = new IntPtr(IntPtr.Zero);
                while (hWnd == IntPtr.Zero && !processes[0].HasExited)
                {
                    Thread.Sleep(100);
                    hWnd = processes[0].MainWindowHandle;
                }
                Thread.Sleep(500);
                SetForegroundWindow(hWnd);
                Rectangle rect = WindowHandler.GetWindowRectFromHandle(hWnd);
                Debug.WriteLine(rect.Y);
                Thread.Sleep(1000);
                Point findIp = ScreenImageHandler.SearchImageOnRegion("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\vpn_ip.png", rect, 0.95);
                if (findIp != new Point(0, 0))
                {
                    MouseHandler.SetCursorPos(findIp.X + 20, findIp.Y + 5);
                    MouseHandler.MouseClick();
                    MouseHandler.MouseClick();
                }
                else
                {
                    findIp = ScreenImageHandler.SearchImageOnRegion("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\vpn_ipActivated.png", rect, 0.95);
                    MouseHandler.SetCursorPos(findIp.X + 20, findIp.Y + 5);
                    MouseHandler.MouseClick();
                    MouseHandler.MouseClick();
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
    }
}
