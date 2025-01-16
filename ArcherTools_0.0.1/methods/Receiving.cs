using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.enums;

namespace ArcherTools_0._0._1.methods
{
    internal class Receiving
    {
        public static void MainCall()
        {
            if (ConfigData._receivingConfig == null) {
                return;
            }

            if (WindowHandler.FindWindow(null, "10.0.1.29 - Remote Desktop Connection") != IntPtr.Zero)
            {
                WindowHandler.WinToFocusByName("mstsc");
            }
            else
            {
                MessageBox.Show("Please open the RDP first, then try again.", ErrorEnum.ErrorCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var windowPos = 0;
            switch (WindowHandler.GetWindowPosition("10.0.1.29 - Remote Desktop Connection").X)
            {
                case >= 3840:
                    windowPos += 3840;
                    break;
                case >= 1920:
                    windowPos += 1920;
                    break;
                default:
                    windowPos = 0;
                    break;
            }

            ReceivingConfig rcvCfg = ConfigData._receivingConfig;
            Rectangle itemSearchRect = rcvCfg.getRectByType(ControlType.ItemSearchWindow).rect.toRectangle();
            Point relativeItemSearchCoords = new Point(255, 93);
            Point screenItemSearchCoords = new Point((itemSearchRect.X + relativeItemSearchCoords.X) + windowPos, itemSearchRect.Y + relativeItemSearchCoords.Y);
            MouseHandler.MouseMoveTo(screenItemSearchCoords);

                        
        }

        public static void TrainCall()
        {
            ToolConfig toolCfg = ConfigData._toolConfig;
            byte PwhMonitor = 1;
//#if !DEBUG
            if (WindowHandler.FindWindow(null, "10.0.1.29 - Remote Desktop Connection") != IntPtr.Zero)
            {
                WindowHandler.WinToFocusByName("mstsc");
            }
            else
            {
                MessageBox.Show("Please open the RDP first, then try again.", ErrorEnum.ErrorCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            switch(WindowHandler.GetWindowPosition("10.0.1.29 - Remote Desktop Connection").X)
            {
                case >= 3840:
                    PwhMonitor = 3;
                    break;
                case >= 1920:
                    PwhMonitor = 2;
                    break;
                default:
                    PwhMonitor = 1;
                    break;
            }
            //#endif

            if (ConfigData._receivingConfig == null)
            {
                ReceivingConfig.createNewRcvCfg();    
            }

            ReceivingConfig rcvConfig = ConfigData._receivingConfig;
            if (rcvConfig == null)
            {
                MessageBox.Show("Receiving Config data is null. Possibly corrupted.", ErrorEnum.ErrorCode.InvalidCfgData.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConfigData.setReceivingConfig(rcvConfig);

            PowerHouseRectangles pwhRect1 = rcvConfig.getRectByType(ControlType.PowerHouseIcons);
            PowerHouseRectangles pwhRect2 = rcvConfig.getRectByType(ControlType.ItemSearchWindow);
            PowerHouseRectangles pwhRect3 = rcvConfig.getRectByType(ControlType.ReceiptLineWindow);
            PowerHouseRectangles pwhRect4 = rcvConfig.getRectByType(ControlType.ItemConfigurationWindow);
            
            List<PowerHouseRectangles> pwhList = new List<PowerHouseRectangles> { pwhRect1, pwhRect2 };
            List<PowerHouseRectangles> alteredRects = RectanglesOverlay.Show(pwhList, PwhMonitor);

            Thread.Sleep(1500);
            DialogResult saveChanges = MessageBox.Show("Would you like to save your changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (saveChanges == DialogResult.Yes)
            {
                ConfigData._receivingConfig.addMousePosition(pwhRect1);
                ConfigData._receivingConfig.addMousePosition(pwhRect2);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig );
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();                
            }
            pwhList.Clear();
            alteredRects.Clear();
            pwhList.Add(pwhRect3);
            pwhList.Add(pwhRect4);
            DialogResult nextStepConfirm = MessageBox.Show("Now we are going to step two.\nPlease open any container's Receipt Lines window, once open, press ok to continue", "Next Step", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (nextStepConfirm != DialogResult.OK) { return; }

            alteredRects = RectanglesOverlay.Show(pwhList, PwhMonitor);

            saveChanges = MessageBox.Show("Would you like to save your changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (saveChanges == DialogResult.Yes)
            {
                ConfigData._receivingConfig.addMousePosition(pwhRect3);
                ConfigData._receivingConfig.addMousePosition(pwhRect4);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();
            }
        }
    }
}
