using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
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
            try
            {
                pwhIcons = ConfigData._receivingConfig.getMousePosByType(ControlType.PowerHouseIcons).getRectangle();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Config not properly set.");
            }
            finally { }
            
            if (pwhIcons == null || pwhIcons.IsEmpty)
            {
                pwhIcons = new Rectangle(0, 0, 150, 150);
            }
            
            Task pwhIconsTsk = Task.Run(() =>
            {
                pwhIcons = OverlayForm.Show(pwhIcons, "Powerhouse Icons");
                Debug.WriteLine(pwhIcons.X);
            });
            Task.WaitAll(pwhIconsTsk);
            Thread.Sleep(10000);
            return;
            SerializableRectangle srlzRect = new SerializableRectangle(pwhIcons);
            PowerHouseRectangles pwhrect = new PowerHouseRectangles(ControlType.PowerHouseIcons, srlzRect);
            List<PowerHouseRectangles> list = new List<PowerHouseRectangles> { pwhrect };            
            ConfigData._receivingConfig.setMousePositions(list);
            ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig );
            cfgData.PrepareForSerialization();
            ConfigData.SerializeConfigData();

        }
    }
}
