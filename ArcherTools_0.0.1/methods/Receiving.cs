using System.Diagnostics;
using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.excel;
using NPOI.SS.Util;

namespace ArcherTools_0._0._1.methods
{
    internal class Receiving
    {
        public static void MainCall()
        {
            try
            {
                if (ConfigData._receivingConfig == null || ConfigData._receivingConfig.RectanglePositionList.Count == 0)
                {
                    throw new Exception("The receiving config is empty, please make sure you have set it up first");
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


                ExcelHandler excelHandler = new ExcelHandler(rcvCfg.ExcelFilePath);
                string rcvDumpSheet = "";
                foreach (var sheet in rcvCfg.ExcelSheetNames)
                {
                    if (sheet == "DUMP")
                    {
                        rcvDumpSheet = sheet;
                        break;
                    }
                }
                List<String> excelValues = excelHandler.GetColumn(rcvDumpSheet, 5, 2);
                Debug.WriteLine($"Excel values count: {excelValues.Count}");
                foreach (var excelValue in excelValues)
                {
                    if (excelValue != null)
                    {
                        Debug.WriteLine(excelValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed task during excel handling at receiving.\n{ex.StackTrace}\n{ex.Message}");
            }



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


            switch (WindowHandler.GetWindowPosition("10.0.1.29 - Remote Desktop Connection").X)
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
            DialogResult saveChanges = MessageBox.Show("Would you like to save these changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (saveChanges == DialogResult.Yes)
            {
                ConfigData._receivingConfig.addMousePosition(pwhRect1);
                ConfigData._receivingConfig.addMousePosition(pwhRect2);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
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

            saveChanges = MessageBox.Show("Would you like to save these changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (saveChanges == DialogResult.Yes)
            {
                ConfigData._receivingConfig.addMousePosition(pwhRect3);
                ConfigData._receivingConfig.addMousePosition(pwhRect4);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();
            }

            DialogResult excelConfirmation = MessageBox.Show("Lastly, we are going to set up your excel that will be used for the item configurations", "Last Step", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (excelConfirmation != DialogResult.OK)
            {
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel Sheet (*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            string filePath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                rcvConfig.setExcelFilePath(filePath);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();
            }
            else
            {
                return;
            }
        }

        public static bool validateConfigData()
        {
            try
            {
                ReceivingConfig rcvCfg = ConfigData._receivingConfig;
                if (rcvCfg == null)
                {
                    ConfigData.createNewCfgFile();
                }
                if (rcvCfg.RectanglePositionList == null)
                {
                    Debug.WriteLine("User has not set up the rectangle positions.");
                    return false;
                }
                if (rcvCfg.ExcelFilePath == null || string.IsNullOrWhiteSpace(rcvCfg.ExcelFilePath))
                {
                    return validateExcel();
                }
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown during validation.\n{ex.StackTrace}\n{ex.Message}");
                return false;
            }
        }

        public static bool validateExcel()
        {
            try
            {
                ReceivingConfig rcvCfg = ConfigData._receivingConfig;
                if (rcvCfg.ExcelFilePath == null || string.IsNullOrWhiteSpace(rcvCfg.ExcelFilePath))
                {
                    bool setexcel = rcvCfg.configExcel();
                    if (!setexcel)
                    {
                        Debug.WriteLine("User rejected setting up excel, or task has failed.");
                        return false;
                    }

                }
                if (rcvCfg.ExcelSheetNames.Count == 0)
                {
                    Debug.WriteLine("Empty sheet.");
                    return false;
                }
                else
                {
                    if (!rcvCfg.ExcelSheetNames.Contains("TEST CHECK"))
                    {
                        Debug.WriteLine("Invalid sheet.");
                        return false;
                    }
                    if (!rcvCfg.ExcelSheetNames.Contains("DUMP"))
                    {
                        Debug.WriteLine("Invalid sheet.");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown during validation.\n{ex.StackTrace}\n{ex.Message}");
                return false;
            }
        }

        public static bool validateRectanglePositions()
        {
            try
            {
                ReceivingConfig rcvCfg = ConfigData._receivingConfig;
                if (rcvCfg.getRectByType(ControlType.ItemSearchWindow).getRectangle() == new Rectangle(0, 0, 150, 150))
                {
                    return false;
                }
                if (rcvCfg.getRectByType(ControlType.ReceiptLineWindow).getRectangle() == new Rectangle(0, 0, 150, 150))
                {
                    return false;
                }
                if (rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle() == new Rectangle(0, 0, 150, 150))
                {
                    return false;
                }
                if (rcvCfg.getRectByType(ControlType.PowerHouseIcons).getRectangle() == new Rectangle(0, 0, 150, 150))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown during validation.\n{ex.StackTrace}\n{ex.Message}");
                return false;
            }
        }
    }
}
