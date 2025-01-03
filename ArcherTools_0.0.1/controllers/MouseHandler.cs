using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ArcherTools_0._0._1.controllers
{

    public enum clickType
    {
        SingleLeftClick,
        SingleRightClick,
        DoubleLeftClick,
        DoubleRightClick,
        MiddleMouseClick
    }

    public enum mouseEventKeys
    {
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        MOUSEEVENTF_LEFTUP = 0x0004
    }
    internal class MouseHandler
    {
        [DllImport("user32.dll")]
        internal static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        internal static void SetCursorPosRelative(int X, int Y)
        {
            Point curMousePos = Control.MousePosition;
            X += curMousePos.X;
            Y += curMousePos.Y;
            SetCursorPos(X, Y);
        }




        internal static void MouseClick(clickType clickType)
        {



            if (clickType == clickType.SingleLeftClick)
            {
                mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
                mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
            }
            else if (clickType == clickType.DoubleLeftClick)
            {
                mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
                mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
                Thread.Sleep(100);
                mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
                mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
            }
        }

        internal static void RelativeMouseDrag(Point startPos, Point endPos, bool snap = false)
        {
            int startPosX = startPos.X;
            int startPosY = startPos.Y;
            int endPosX = endPos.X;
            int endPosY = endPos.Y;
            int stepsX = endPosX - startPosX;
            int stepsY = endPosY - startPosY;
            int step = 10;

            mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            if (snap)
            {         
                SetCursorPos(endPosX, endPosY);

                Thread.Sleep(50);                
            }
            else
            {
                while (startPosX != endPosX || startPosY != endPosY)
                {
                    int nextX = startPosX + Math.Sign(endPosX - startPosX) * Math.Min(step, Math.Abs(endPosX - startPosX));
                    int nextY = startPosY + Math.Sign(endPosY - startPosY) * Math.Min(step, Math.Abs(endPosY - startPosX));
                    
                    SetCursorPos(nextX, nextY);
                    
                    startPosX = nextX;
                    startPosY = nextY;
                    
                    Thread.Sleep(1);

                    SetCursorPos(startPosX, startPosY);
                }

            }
            Debug.WriteLine("Mouse drag finished.");
            mouse_event((uint)mouseEventKeys.MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);

        }
    }
}