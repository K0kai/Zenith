﻿using System.Diagnostics;
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
        internal static Point receiptLineBorder = new Point(18, 33);
        internal static Point receiptLnFirstLn = new Point(55, 41);
        internal static Point itemSearchBox = new Point(190, 90);
        internal static Point itemMtnIcon = new Point(355, 11);
        internal static Point itemCfgIcon = new Point(264, 10);
        internal static Point itemCfgPcsBox = new Point(160, 38);

        internal static Point rlReceiptLineBorder = new Point();
        internal static Point rlReceiptLnFirstLn = new Point();
        internal static Point rlItemSearchBox = new Point();
        internal static Point rlItemMtnIcon = new Point();
        internal static Point rlItemCfgIcon = new Point();
        internal static Point rlItemCfgPcsBox = new Point();

        internal static List<Item> receivedItems = new List<Item>();
        internal static Dictionary<int, Item> failedItems = new Dictionary<int, Item>();

        internal static int pwhMonitor;

        internal static int baseDelay = 500;

        
        public static void MainCall()
        {
            var validConfig = validateConfigData();
            var validRect = validateRectanglePositions();
            var validExcel = validateExcel();
            if (validConfig && validRect && validExcel)
            {
                ReceivingConfig rcvCfg = ConfigData._receivingConfig;

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
                        pwhMonitor = 3840;
                        break;
                    case >= 1920:
                        pwhMonitor = 1920;
                        break;
                    default:
                        pwhMonitor = 0;
                        break;
                }
                rlReceiptLineBorder = toRelativePoint(rcvCfg.getRectByType(ControlType.ReceiptLineWindow), receiptLineBorder);
                rlReceiptLnFirstLn = toRelativePoint(rcvCfg.getRectByType(ControlType.ReceiptLineWindow), receiptLnFirstLn);
                rlItemSearchBox = toRelativePoint(rcvCfg.getRectByType(ControlType.ItemSearchWindow), itemSearchBox);
                rlItemMtnIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseIcons), itemMtnIcon);
                rlItemCfgIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseIcons), itemCfgIcon);
                rlItemCfgPcsBox = toRelativePoint(rcvCfg.getRectByType(ControlType.ItemConfigurationWindow), itemCfgPcsBox);
                Point rlItemMtnClose = new Point(pwhMonitor + rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Location.X + rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Width - 20, rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Location.Y + 15);
                Point rlItemCfgClose = new Point(pwhMonitor + rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Location.X + rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Width - 15, rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Location.Y + 15);


                ExcelHandler excelHandler = new ExcelHandler(rcvCfg.ExcelFilePath);
                
                    {
                        string mainWorkSheet = "";
                        string rcvDumpSheet = "";
                        foreach (var sheet in rcvCfg.ExcelSheetNames)
                        {
                            if (sheet == "DUMP")
                            {
                                rcvDumpSheet = sheet;
                                break;
                            }
                            if (sheet == "TEST CHECK")
                            {
                                mainWorkSheet = "TEST CHECK";
                            }
                        }
                        excelHandler.SetCell("TEST CHECK", 10, 3, 1);
                        var cellvalue = excelHandler.GetCell("TEST CHECK", 13, 4);
                        Debug.WriteLine($"Unaltered value: {cellvalue}");
                        excelHandler.SetCell("TEST CHECK", 10, 3, 5);
                        cellvalue = excelHandler.GetCell("TEST CHECK", 13, 4);
                        Debug.WriteLine($"Altered value: {cellvalue}");
                        MouseHandler.MouseMoveTo(rlReceiptLnFirstLn);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                        MouseHandler.MouseClick();
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                        KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Tab);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                        KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.C, KeysEnum.SendKey.Ctrl);
                        int startLine = 1;
                        Debug.WriteLine(containerSize());
                        for (int i = startLine; i <= containerSize(); i++)
                        {
                            excelHandler.SetCell(mainWorkSheet, 10, 3, i);
                            Dictionary<string, string> currentItemInfo = new Dictionary<string, string>
                    {
                        {"number_pieces", excelHandler.GetCell(mainWorkSheet, 4, 3) },
                        {"cases_per_pallet", excelHandler.GetCell(mainWorkSheet, 4, 8) },
                        {"cases_per_tier", excelHandler.GetCell(mainWorkSheet, 6, 8) },
                        {"pallet_weight", excelHandler.GetCell(mainWorkSheet, 13, 4)},
                        {"pallet_height", excelHandler.GetCell(mainWorkSheet, 15, 4) },
                        {"pallet_width", excelHandler.GetCell(mainWorkSheet, 17, 4) },
                        {"pallet_depth", excelHandler.GetCell(mainWorkSheet, 19, 4) },
                        {"case_weight" , excelHandler.GetCell(mainWorkSheet, 13, 9) },
                        {"case_height", excelHandler.GetCell(mainWorkSheet, 15, 9) },
                        {"case_width", excelHandler.GetCell(mainWorkSheet , 17, 9) },
                        {"case_depth", excelHandler.GetCell(mainWorkSheet, 19, 9) }
                    };

                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            MouseHandler.MouseMoveTo(rlReceiptLineBorder);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            MouseHandler.MouseClick();
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                            KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.C, KeysEnum.SendKey.Ctrl);
                            var checkItem = iterateThroughList();
                            var copiedItem = Clipboard.GetText();
                            if (!checkItem)
                            {
                                MouseHandler.MouseMoveTo(rlItemSearchBox); MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                                string currentItemCode = Clipboard.GetText();
                                KeystrokeHandler.TypeText(currentItemCode);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                                KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Enter);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemMtnIcon); MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemCfgIcon); MouseHandler.MouseClick();

                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                var pcCheck = checkPCs(currentItemInfo["number_pieces"]);
                            //Start Item Config
                            if (pcCheck)
                            {
                                //Tabbing
                                for (int tab = 1; tab <= 3; tab++)
                                {
                                    KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Tab);
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.75));
                                }

                                KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Backspace);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                KeystrokeHandler.TypeText(currentItemInfo["cases_per_pallet"]);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.75));
                                KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Tab);
                                KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Backspace);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                KeystrokeHandler.TypeText(currentItemInfo["cases_per_tier"]);
                                for (int tab = 1; tab <= 7; tab++)
                                {
                                    KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Tab);
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.75));
                                }
                            }
                            else
                            {
                                var newItem = new Item(copiedItem);
                                failedItems.Add(i, newItem);
                            }
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.S, KeysEnum.SendKey.Ctrl);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemCfgClose); MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemMtnClose); MouseHandler.MouseClick();

                            }
                        }
                    }
                }
            else
            {
                throw new Exception("Config is invalid, cannot continue receiving.");
            }
        }

        private static int containerSize()
        {
            int size = 0;
            ExcelHandler exHandler = new ExcelHandler(ConfigData._receivingConfig.ExcelFilePath);
            string worksheetName = ConfigData._receivingConfig.ExcelSheetNames[1];
            size = exHandler.GetColumn(worksheetName, 4, 2).Count();
            return size;
        }

        private static bool checkPCs(string pcs)
        {
            
            int intPcs = int.Parse(pcs);
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.8));
                    KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.C, KeysEnum.SendKey.Ctrl);
                    var charsToRemove = new string[] { "P", "C", "S", "p", "c", "s" };
                    string copiedPcs = Clipboard.GetText();
                    foreach (string c in charsToRemove)
                    {
                        copiedPcs = copiedPcs.Replace(c, string.Empty);
                    }
                    int intCopiedPcs = int.Parse(copiedPcs);
                    Debug.WriteLine(intPcs);
                    Debug.WriteLine(intCopiedPcs);
                    if (intPcs == intCopiedPcs)
                    {
                        Debug.WriteLine("Match");
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Catched {ex.Message} at PCs matching.", ex);
                }
            }
            return false;

        }

        private static Point toRelativePoint(PowerHouseRectangles rect, Point relative)
        {
            if (!validateConfigData() && !validateRectanglePositions())
            {
                throw new InvalidDataException("Incorrect config set up for this operation.");
            }
            ReceivingConfig rcvCfg = ConfigData._receivingConfig;

            Point relativePoint = new Point();
            Rectangle convRect = rect.getRectangle();
            int X = pwhMonitor + convRect.Location.X + relative.X;
            int Y = convRect.Location.Y + relative.Y;
            relativePoint = new Point(X, Y);
            return relativePoint;

        }
        private static bool iterateThroughList()
        {
            int numIteration = 0;
            bool found = false;
            var copiedItem = Clipboard.GetText();
            while (true)
            {
                found = false;
                if (numIteration >= 10)
                {
                    return false;
                }
                if (receivedItems.Count == 0)
                {
                    var newIt = new Item(copiedItem);
                    receivedItems.Add(newIt);
                    break;
                }
                else
                {
                    foreach( var item in receivedItems)
                    {
                        if (item.itemCode == copiedItem)
                        {
                            found = true;
                            Debug.WriteLine($"{item.itemCode} vs {copiedItem}");
                        }
                    }
                    if (found == true)
                    {
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                        KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Down);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                        KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.C, KeysEnum.SendKey.Ctrl);
                        copiedItem = Clipboard.GetText();
                        numIteration++;
                    }
                    else
                    {
                        var newIt = new Item(copiedItem);
                        receivedItems.Add(newIt);
                        found = false;
                        return found;
                    }
                }
            }

            return found;
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
            PowerHouseRectangles pwhRect5 = rcvConfig.getRectByType(ControlType.ItemMaintenanceWindow);

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
            pwhList.Add(pwhRect5);
            DialogResult nextStepConfirm = MessageBox.Show("Now we are going to step two.\nPlease open any container's Receipt Lines window, once open, press ok to continue", "Next Step", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (nextStepConfirm != DialogResult.OK) { return; }

            alteredRects = RectanglesOverlay.Show(pwhList, PwhMonitor);

            saveChanges = MessageBox.Show("Would you like to save these changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (saveChanges == DialogResult.Yes)
            {
                ConfigData._receivingConfig.addMousePosition(pwhRect3);
                ConfigData._receivingConfig.addMousePosition(pwhRect4);
                ConfigData._receivingConfig.addMousePosition(pwhRect5);
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
