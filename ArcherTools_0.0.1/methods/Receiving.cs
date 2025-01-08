using ArcherTools_0._0._1.boxes;
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
               
                PowerHouseRectangles MousePos1 = config.getMousePosByType(ControlType.ItemSearchInquiry);
                var MousePos2 = MousePos1.getPosition();
                WindowHandler.WinToFocusByName("mstsc");
              
                                        
                
            }
            
        }

        public static void TrainCall()
        {
            WindowHandler.WinToFocusByName("mstsc");
            Rectangle pwhIcons = new Rectangle();
            Task pwhIconsTsk = Task.Run(() =>
            {
                pwhIcons = OverlayForm.Show(new Rectangle(0, 0, 150, 500), "Powerhouse Icons");
            });
            Task.WaitAll(pwhIconsTsk);
            OverlayForm.Show(pwhIcons, "Powerhouse Icons 2");

        }
    }
}
