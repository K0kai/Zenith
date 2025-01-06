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
            if (ConfigData._receivingConfig != null)
            {
                ReceivingConfig config = ConfigData._receivingConfig;
               
                MousePosition MousePos1 = config.getMousePosByType(ControlType.ItemSearchInquiry);
                var MousePos2 = MousePos1.getPosition();
                Point posToPoint = new Point (MousePos2[0], MousePos2[1] - 10);
                WindowHandler.WinToFocusByName("mstsc");
                MouseHandler.SetCursorPos(posToPoint.X, posToPoint.Y);
                MouseHandler.MouseClick();
                MouseHandler.SetCursorPos(MousePos1.X(), MousePos1.Y());
                MouseHandler.MouseClick();
                                        
                
            }
            
        }
    }
}
