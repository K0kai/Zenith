using System.Diagnostics;
using System.Runtime.InteropServices;
using InputSimulatorEx;
using NPOI.SS.Formula.Functions;

namespace ArcherTools_0._0._1.controllers
{

    public enum clickType
    {
        SingleLeftClick,
        SingleLeftClickHold,
        SingleLeftClickRelease,
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
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        static InputSimulator sim = new InputSimulator();

        internal static void SetCursorPosRelative(int X, int Y)
        {
            Point curMousePos = Control.MousePosition;
            X += curMousePos.X;
            Y += curMousePos.Y;
            SetCursorPos(X, Y);
        }

        internal static Point pointFromList(List<int> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new Exception("The list is empty, unable to make point.");                
            }
            Point convPoint = new Point(list[0], list[1]);
            return convPoint;
        }
        internal static void MouseMoveTo(Point coords)
        {
            SetCursorPos(coords.X, coords.Y);
        }
        internal static void MouseClick(clickType clkType = clickType.SingleLeftClick)
        {
            switch (clkType)
            {
                case clickType.SingleLeftClickHold:
                    sim.Mouse.LeftButtonDown();                    
                    break;
                case clickType.SingleLeftClickRelease:
                    sim.Mouse.LeftButtonUp();
                    break;
                case clickType.DoubleLeftClick:
                    sim.Mouse.LeftButtonDoubleClick();
                    break;
                case clickType.SingleRightClick:
                    sim.Mouse.RightButtonClick();
                    break;
                default:
                    sim.Mouse.LeftButtonClick();
                    break;
            }
            Thread.Sleep(100);
        }

        internal static void RelativeMouseDrag(Point startPos, Point endPos, bool snap = false)
        {
            int startPosX = startPos.X;
            int startPosY = startPos.Y;
            int endPosX = endPos.X;
            int endPosY = endPos.Y;
            int stepsX = endPosX - startPosX;
            int stepsY = endPosY - startPosY;
            int stepX = 10;
            int stepY = 2;

            MouseClick(clickType.SingleLeftClickHold);
            if (snap)
            {         
                SetCursorPos(endPosX, endPosY);

                Thread.Sleep(50);                
            }
            else
            {
                while (startPosX != endPosX || startPosY != endPosY)
                {
                    int nextX = startPosX + Math.Sign(endPosX - startPosX) * Math.Min(stepX, Math.Abs(endPosX - startPosX));
                    int nextY = startPosY + Math.Sign(endPosY - startPosY) * Math.Min(stepY, Math.Abs(endPosY - startPosX));
                    
                    SetCursorPos(nextX, nextY);
                    
                    startPosX = nextX;
                    startPosY = nextY;
                    
                    Thread.Sleep(1);

                    SetCursorPos(startPosX, startPosY);
                }

            }
            MouseClick(clickType.SingleLeftClickRelease);
            Debug.WriteLine("Mouse drag finished.");
           

        }
    }
}