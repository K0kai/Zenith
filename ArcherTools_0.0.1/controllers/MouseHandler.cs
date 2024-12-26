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
    }
}
