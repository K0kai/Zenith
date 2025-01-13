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
                        
        }

        public static void TrainCall()
        {
            ToolConfig toolCfg = ConfigData._toolConfig;
            byte PwhMonitor = 1;
            if (toolCfg != null) {
                try
                {
                    if (toolCfg.PowerHouseMonitor != null || toolCfg.PowerHouseMonitor <= 0)
                    {
                        PwhMonitor = toolCfg.PowerHouseMonitor;
                    }
                }
                catch { }
                finally { }
            }
            WindowHandler.WinToFocusByName("mstsc");
            PowerHouseRectangles pwhRect1 = new PowerHouseRectangles(ControlType.PowerHouseIcons, new SerializableRectangle(new Rectangle(0,0,150,150)));
            PowerHouseRectangles pwhRect2 = new PowerHouseRectangles(ControlType.ItemSearchWindow, new SerializableRectangle(new Rectangle(300, 500, 150, 150)));
            ReceivingConfig rcvConfig = ConfigData._receivingConfig;
            if (rcvConfig.findRectByType(ControlType.ItemSearchWindow) != null && !rcvConfig.findRectByType(ControlType.ItemSearchWindow).getRectangle().IsEmpty) 
            {
                pwhRect2 = rcvConfig.findRectByType(ControlType.ItemSearchWindow);
            }
            if (rcvConfig.findRectByType(ControlType.PowerHouseIcons) != null && !rcvConfig.findRectByType(ControlType.PowerHouseIcons).getRectangle().IsEmpty)
            {
                pwhRect1 = rcvConfig.findRectByType(ControlType.PowerHouseIcons);
            }
            List<PowerHouseRectangles> pwhList = new List<PowerHouseRectangles> { pwhRect1, pwhRect2 };
            List<PowerHouseRectangles> alteredRects = RectanglesOverlay.Show(pwhList, PwhMonitor);

            Thread.Sleep(1500);            
            DialogResult saveChanges = MessageBox.Show("Would you like to save your changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (saveChanges == DialogResult.Yes)
            {
                ConfigData._receivingConfig.setMousePositions(alteredRects);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig );
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();                
            }

        }
    }
}
