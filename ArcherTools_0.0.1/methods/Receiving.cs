using System.Collections.Concurrent;
using System.Diagnostics;
using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.forms;
using InputSimulatorEx;

namespace ArcherTools_0._0._1.methods
{
    internal class Receiving
    {
        internal static Point receiptLineBorder = new Point(18, 33);
        internal static Point receiptLnFirstLn = new Point(55, 52);
        internal static Point itemSearchBox = new Point(190, 90);
        internal static Point itemMtnIcon = new Point(355, 54);
        internal static Point itemCfgIcon = new Point(264, 54);
        internal static Point itemCfgPcsBox = new Point(160, 38);

        internal static Point rlReceiptLineBorder = new Point();
        internal static Point rlReceiptLnFirstLn = new Point();
        internal static Point rlItemSearchBox = new Point();
        internal static Point rlItemMtnIcon = new Point();
        internal static Point rlItemCfgIcon = new Point();
        internal static Point rlItemCfgPcsBox = new Point();

        internal static List<ControlType> requiredCtrlTypes = new List<ControlType> { ControlType.ReceiptLineWindow, ControlType.ItemSearchWindow, ControlType.PowerHouseUpperTab, ControlType.ItemConfigurationWindow };

        internal static List<int> linesFromExcel;
        internal static List<Item> receivedItems = new List<Item>();
        internal static ConcurrentDictionary<int, Item> failedItems = new ConcurrentDictionary<int, Item>();

        internal static int pwhMonitor;
        internal static int baseDelay = 500;
        internal static bool endProcess = false;

        private static void PrepareToReceive(ExcelHandler exHandler, string worksheetName)
        {
            List<int> listLine = new List<int>();
            var lineCol = exHandler.GetColumn(worksheetName, 3, 2);
            listLine = lineCol.Select(int.Parse).ToList();
            listLine.Sort();
            linesFromExcel = listLine;            
        }

        public static async void MainCall(int startLine = 1, bool descendingStart = false)
        {
            var validConfig = validateConfigData();
            var validRect = validateRectanglePositions();
            var validExcel = validateExcel();
            if (validConfig && validRect && validExcel)
            {
                endProcess = false;
                ReceivingConfig rcvCfg = ConfigData._receivingConfig;
                ReceivingGUI rcvGui = ReceivingGUI._instance;

                rcvGui.updateStatusLabel("Beginning receiving process...");
                var autoCreateCfg = ConfigData._toolConfig.AutomaticCreateConfig;
                var findDefaultCfg = ConfigData._toolConfig.CheckForDefault;
                

                if (WindowHandler.FindWindow(null, "10.0.1.29 - Remote Desktop Connection") != IntPtr.Zero)
                {
                    WindowHandler.WinToFocusByName("mstsc");
                }
                else
                {
                    MessageBox.Show("Please open the RDP first, then try again.", ErrorEnum.ErrorCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rcvGui.updateStatusLabel("Receiving Status: Discontinued.");
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
                rlItemMtnIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemMtnIcon);
                rlItemCfgIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemCfgIcon);
                rlItemCfgPcsBox = toRelativePoint(rcvCfg.getRectByType(ControlType.ItemConfigurationWindow), itemCfgPcsBox);
                Point rlItemMtnClose = new Point(pwhMonitor + rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Location.X + rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Width - 20, rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Location.Y + 15);
                Point rlItemCfgClose = new Point(pwhMonitor + rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Location.X + rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Width - 15, rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Location.Y + 15);

                var inputSimulator = new InputSimulator();


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
                            mainWorkSheet = sheet;
                        }
                    }
                    if (descendingStart)
                    {
                        linesFromExcel.Reverse();
                    }
                    PrepareToReceive(excelHandler, rcvDumpSheet);
                    excelHandler.SetCell(mainWorkSheet, 10, 3, 1);
                    var cellvalue = excelHandler.GetCell(mainWorkSheet, 13, 4);
                    excelHandler.SetCell(mainWorkSheet, 10, 3, 5);
                    cellvalue = excelHandler.GetCell(mainWorkSheet, 13, 4);                   
                    if (linesFromExcel == null || linesFromExcel.Count == 0)
                    {
                        rcvGui.updateStatusLabel("Receiving Interrupted: No lines were found in excel.");
                        return;
                    }
                    var iteration = 1;
                    if (startLine > 1)
                    {
                        iteration = startLine;
                    }
                    else
                    {
                        startLine = int.Parse(excelHandler.GetCell(rcvDumpSheet,2,3));
                    }
                    int cntSize = containerSize();
                    int cntRawSize = containerSize(true);
                    Debug.WriteLine(cntSize);
                    MouseHandler.MouseMoveTo(new Point(rlReceiptLnFirstLn.X + 30,rlReceiptLnFirstLn.Y));
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                    MouseHandler.MouseClick();
                    inputSimulator.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.SHIFT);
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.15));
                    inputSimulator.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.CONTROL);
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.15));
                    inputSimulator.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.MENU);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    Task.Run(() =>
                    {
                        _ = checkForEnd();
                    });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed


                    foreach (var line in linesFromExcel)
                    {
                        try {
                            rcvGui.updateVisibility(rcvGui.overlayTip_lbl, true);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            MouseHandler.MouseMoveTo(rlReceiptLnFirstLn);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                            MouseHandler.MouseClick();
                            inputSimulator.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_C);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                            //var checkItem = iterateThroughListItems();
                            rcvGui.updateStatusLabel($"Receiving Item: {iteration} out of {cntRawSize}");
                            var checkItem = iterateThroughListLines(cntSize, line);  
                            if (endProcess) { AfterEndProcess(rcvGui, line); return; }
                            excelHandler.SetCell(mainWorkSheet, 10, 3, line);
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
                            var skipItem = false;
                            var copiedItem = Clipboard.GetText();
                            foreach (var itemInfoValue in currentItemInfo)
                            {
                                if (itemInfoValue.Value == "#VALUE!" || itemInfoValue.Value == "#VALOR!" || itemInfoValue.Value == "#N/D")
                                {                                    
                                    var newFailedItem = new Item(copiedItem);
                                    failedItems.TryAdd(line, newFailedItem);
                                    skipItem = true;
                                    Debug.WriteLine("Invalid Item.");
                                    break;
                                }
                            }
                            foreach (var item in receivedItems)
                            {
                                if (item.itemCode == copiedItem)
                                {
                                    skipItem = true;
                                    Debug.WriteLine("Equal Item, skipping.");
                                }
                            }
                            if (skipItem)
                            {                                
                                iteration++;
                                continue;
                            }
                            if (endProcess) { AfterEndProcess(rcvGui, line); return; }
                            if (checkItem)
                            {
                                MouseHandler.MouseMoveTo(rlItemSearchBox); MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                                string currentItemCode = Clipboard.GetText();
                                KeystrokeHandler.TypeText(currentItemCode);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.25));
                                inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemMtnIcon); MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemCfgIcon); MouseHandler.MouseClick();

                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                if (endProcess) { AfterEndProcess(rcvGui, line); return; }
                                bool pcCheck;
                                if (findDefaultCfg)
                                {
                                    pcCheck = checkPCsForDefault();
                                }
                                else
                                {
                                    pcCheck = checkPCs(currentItemInfo["number_pieces"]);
                                }
                                //Start Item Config
                                if (pcCheck)
                                {
                                    
                                    //Tabbing
                                    tabBetween(3);
                                    inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.BACK);
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                    KeystrokeHandler.TypeText(currentItemInfo["cases_per_pallet"]);
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.75));
                                    inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.TAB);
                                    inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.BACK);
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                    KeystrokeHandler.TypeText(currentItemInfo["cases_per_tier"]);
                                    if (endProcess) { AfterEndProcess(rcvGui, line);  return; }                                   
                                    tabBetween(7);
                                    KeystrokeHandler.TypeText(currentItemInfo["pallet_weight"]);
                                    tabBetween(2);
                                    KeystrokeHandler.TypeText(currentItemInfo["pallet_height"]);
                                    tabBetween(2);
                                    KeystrokeHandler.TypeText(currentItemInfo["pallet_width"]);
                                    tabBetween(2);
                                    KeystrokeHandler.TypeText(currentItemInfo["pallet_depth"]);
                                    tabBetween(4);
                                    KeystrokeHandler.TypeText(currentItemInfo["case_weight"]);
                                    tabBetween(2);
                                    KeystrokeHandler.TypeText(currentItemInfo["case_height"]);
                                    tabBetween(2);
                                    KeystrokeHandler.TypeText(currentItemInfo["case_width"]);
                                    tabBetween(2);
                                    KeystrokeHandler.TypeText(currentItemInfo["case_depth"]);
                                    if (endProcess) { AfterEndProcess(rcvGui, line);  return; }

                                }
                                else
                                {
                                    if (!autoCreateCfg)
                                    {
                                        var newItem = new Item(copiedItem);
                                        failedItems.TryAdd(line, newItem);
                                        rcvGui.updateStatusLabel($"Status: No pieces matching the config was found. Line: {line}");
                                    }
                                    else
                                    {
                                        if (endProcess) { AfterEndProcess(rcvGui, line);  return; }
                                        CreateNewConfig(int.Parse(currentItemInfo["number_pieces"]), inputSimulator);
                                        if (endProcess) { AfterEndProcess(rcvGui, line);  return; }
                                        tabBetween(3);
                                        inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.BACK);
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                        KeystrokeHandler.TypeText(currentItemInfo["cases_per_pallet"]);
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.75));
                                        inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.TAB);
                                        inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.BACK);
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));                                        
                                        KeystrokeHandler.TypeText(currentItemInfo["cases_per_tier"]);
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                        inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.TAB);
                                        inputSimulator.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.BACK);
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
                                        KeystrokeHandler.TypeText(currentItemInfo["number_pieces"]);
                                        tabBetween(6);
                                        KeystrokeHandler.TypeText(currentItemInfo["pallet_weight"]);
                                        tabBetween(2);
                                        KeystrokeHandler.TypeText(currentItemInfo["pallet_height"]);
                                        tabBetween(2);
                                        KeystrokeHandler.TypeText(currentItemInfo["pallet_width"]);
                                        tabBetween(2);
                                        KeystrokeHandler.TypeText(currentItemInfo["pallet_depth"]);
                                        tabBetween(4);
                                        KeystrokeHandler.TypeText(currentItemInfo["case_weight"]);
                                        tabBetween(2);
                                        KeystrokeHandler.TypeText(currentItemInfo["case_height"]);
                                        tabBetween(2);
                                        KeystrokeHandler.TypeText(currentItemInfo["case_width"]);
                                        tabBetween(2);
                                        KeystrokeHandler.TypeText(currentItemInfo["case_depth"]);
                                        if (endProcess) { AfterEndProcess(rcvGui, line);  return; }
                                    }
                                }
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));

                                if (endProcess) { AfterEndProcess(rcvGui, line);  return; }

                                inputSimulator.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_S);
                                var newIt = new Item(copiedItem);
                                receivedItems.Add(newIt);
                                iteration++;
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemCfgClose); MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemMtnClose); MouseHandler.MouseClick();

                            }
                        }
                        catch(Exception ex)
                        {
                            rcvGui.updateStatusLabel($"Status: An error ocurred, ending receiving now\nDetails: {ex.StackTrace}\n{ex.Message}");
                            if (failedItems.Count <= 0)
                            {
                                receivingCleanUp();
                                Debug.WriteLine("cleaning all items");
                            }
                            else
                            {
                                receivingCleanUp(false);
                                Debug.WriteLine("cleaning all except failed");
                            }
                            return;
                        }
                        }
                    string statusTxt = "";
                        if (endProcess)
                        {
                            statusTxt += "Receiving Incomplete: ";
                        }
                        else {
                            statusTxt += "Receiving Complete: ";
                        }
                        if (failedItems.Count <= 0)
                        {
                            statusTxt += "There were no failed items.";
                        }
                        else
                        {
                            statusTxt += $"There were {failedItems.Count} failed items.";
                        foreach (var item in failedItems)
                        {
                            Debug.WriteLine($"Item {item.Value.itemCode} failed at line: {item.Key}.");
                        }
                        }
                        rcvGui.updateStatusLabel(statusTxt);
                        if (failedItems.Count <= 0)
                    {
                        receivingCleanUp();
                    }
                    else
                    {
                        receivingCleanUp(false);
                    }
                        
                    }
                
                }
            else
            {
                throw new Exception("Config is invalid, cannot continue receiving.");
            }
            }

        private static void CreateNewConfig(int pcs, InputSimulator ips)
        {
            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
            ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.INSERT);
            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.3));
            ips.Keyboard.TextEntry($"{pcs}PC");
        }
        private static int containerSize(bool rawSize = false)
        {
            try
            {
                var size = 0;
                ExcelHandler exHandler = new ExcelHandler(ConfigData._receivingConfig.ExcelFilePath);
                string worksheetName = ConfigData._receivingConfig.ExcelSheetNames[1];
                var Column = exHandler.GetColumn(worksheetName, 3, 2);
                if (Column.Count <= 0)
                {
                    throw new IndexOutOfRangeException("Item array cannot be zero or less than zero");
                }
                if (rawSize)
                {
                    size = Column.Count();
                    return size;
                }
                else
                {
                    var highestNumber = 0;
                    foreach (var row in Column)
                    {
                        if (int.Parse(row) > highestNumber)
                        {
                            
                            highestNumber = int.Parse(row);
                            Debug.WriteLine(row);

                        }
                    }
                    return highestNumber;
                }
            }
            catch (Exception ex)
            {
                ReceivingGUI rcvGui = ReceivingGUI._instance;
                rcvGui.updateStatusLabel($"Status: Failed to get container size.\nDetails: {ex.StackTrace} | {ex.Message}");
                return -1;
            }
        }

        private static Task UpdateReceivedItems(int Line, Item Item, bool fail = false)
        {
            if (!fail)
            {
                try
                {
                    receivedItems.Add(Item);
                    if (Container.SelectedContainer != null)
                    {
                        var currentContainer = Container.SelectedContainer;
                        currentContainer.AddItemToRelease(Container.SelectedRelease, Line, Item);
                    }
                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }
            }
            else
            {
                try
                {
                    failedItems.TryAdd(Line, Item);
                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }
            }
        }

        private static void AfterEndProcess(ReceivingGUI rcvGuiInstance, int Line)
        {
            rcvGuiInstance.updateStatusLabel($"Receiving Interrupted: Stopped at Line: {Line}");
            rcvGuiInstance.updateVisibility(rcvGuiInstance.overlayTip_lbl, false);
        }
        private static bool checkForEnd()        {
            InputSimulator ips = new InputSimulator();            
            while (true)
            {
                
                if (ips.InputDeviceState.IsHardwareKeyDown(InputSimulatorEx.Native.VirtualKeyCode.END))
                {
                    Debug.WriteLine("pressed");
                    endProcess = true;
                    return true;
                }
                Thread.Sleep(25);

            }
            
        }

        private static void tabBetween(int times, int start = 1)
        {
            var inputSim = new InputSimulator();
            for (int i = start; i <= times; i++)
            {
                inputSim.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.TAB);
                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.4));
            }
        }
        

        private static bool checkPCsForDefault()
        {
            Thread.Sleep((int)Math.Ceiling(baseDelay * 1.2));
            for (int i = 0; i < 5; i++)
            {
                Point mouseto = new Point(0, 0);
                Task findDefault = Task.Run(() =>
                {
                    mouseto = ScreenImageHandler.SearchImageOnScreen("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\defaultcfg.png", 0.99);
                });
                Task.WaitAll(findDefault);
                if (mouseto == new Point(0, 0))
                {
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.4));
                    KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Down);
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                }
                else
                {
                    return true;
                }
            }
            return false;
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
                    Debug.WriteLine($"Cfg pcs: {intPcs} vs Copied pcs: {intCopiedPcs}");
                    if (intPcs == intCopiedPcs)
                    {
                        Debug.WriteLine("Matched");
                        return true;

                    }
                    else
                    {
                        KeystrokeHandler.sendKeystroke(KeysEnum.SendKey.Down);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Caught {ex.Message} at pieces matching.", ex);
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

        private static bool iterateThroughListLines(int containerSize, int neededLine)
        {
            
            InputSimulator ips = new InputSimulator();
            int numIteration = 0;

            while (true)
            {
                try {  
                        for (int i = 1; i <= containerSize; i++)
                        {
                        if (numIteration >= 100 || endProcess)
                        {
                            endProcess = true;
                            throw new Exception("Iteration timeout");
                        }
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.2));
                            ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_C);
                            var copiedLine = int.Parse(Clipboard.GetText());
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.10));
                            if (copiedLine == neededLine)
                            {
                            var lineMatch = false;
                            for (int x = 1; x <= 3; x++)
                            {
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.15));
                                ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_C);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.2));
                                copiedLine = int.Parse(Clipboard.GetText());
                                if (copiedLine != neededLine)
                                {
                                    lineMatch = false;
                                    break;
                                }
                                else { lineMatch = true;}
                            }
                            if (!lineMatch)
                            {
                                continue;
                            }
                                EnsureUnstuckKeys();
                                Clipboard.Clear();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.1));
                                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.TAB);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.1));

                                for (int z = 1; z <= 3; z++)
                                {
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.4));
                                    ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_C);
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.15));
                                }                                
                                
                                return true;
                            }
                            else if (copiedLine < neededLine)
                            {
                                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.DOWN);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.1));
                            }
                            else if (copiedLine > neededLine)
                            {
                                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.UP);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.1));
                            }
                        }
                     

                } catch (Exception ex)
                {
                    if (ex.Source == "InputSimulatorEx.dll")
                    {
                        Debug.WriteLine("its the ips");
                        return false;
                    }
                    if (ex.Message == "Iteration timeout")
                    {
                        return false;
                    }
                    Debug.WriteLine($"Catched Exception {ex}, line:{Clipboard.GetText()} at line iterating");
                }

            }
        }

        

        private static void EnsureUnstuckKeys()
        {
            InputSimulator ips = new InputSimulator();
            ips.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.CONTROL);
            Thread.Sleep(200);
            ips.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.SHIFT);
            Thread.Sleep(200);
            ips.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
        }
       /* private static bool iterateThroughListItems()
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
                    foreach( var Item in receivedItems)
                    {
                        if (Item.itemCode.Trim() == copiedItem.Trim())
                        {
                            found = true;
                            Debug.WriteLine($"Already received Item: {Item.itemCode} vs copied Item: {copiedItem}");
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
        }  */   

        public static void TrainCall()
        {
            ToolConfig toolCfg = ConfigData._toolConfig;
            byte PwhMonitor = 1;
           // #if !DEBUG
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
           // #endif

            ReceivingConfig rcvConfig = ConfigData._receivingConfig;

            if (rcvConfig == null)
            {
                MessageBox.Show("Receiving Config data is null. Possibly corrupted.", ErrorEnum.ErrorCode.InvalidCfgData.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                rcvConfig = new ReceivingConfig();
            }
            ConfigData.setReceivingConfig(rcvConfig);
            

            PowerHouseRectangles pwhRect1 = new PowerHouseRectangles(rcvConfig.getRectByType(ControlType.PowerHouseUpperTab));
            PowerHouseRectangles pwhRect2 = new PowerHouseRectangles(rcvConfig.getRectByType(ControlType.ItemSearchWindow));
            PowerHouseRectangles pwhRect3 = new PowerHouseRectangles(rcvConfig.getRectByType(ControlType.ReceiptLineWindow));
            PowerHouseRectangles pwhRect4 = new PowerHouseRectangles(rcvConfig.getRectByType(ControlType.ItemConfigurationWindow));
            PowerHouseRectangles pwhRect5 = new PowerHouseRectangles(rcvConfig.getRectByType(ControlType.ItemMaintenanceWindow));

            ReceivingConfig newRcvCfg = new ReceivingConfig(rcvConfig);
            List<PowerHouseRectangles> pwhList = new List<PowerHouseRectangles> { pwhRect1, pwhRect2 };
            List<PowerHouseRectangles> alteredRects = new List<PowerHouseRectangles>();
            Task rectsPt1 = Task.Run(() =>
            {
                 alteredRects = RectanglesOverlay.Show(pwhList, PwhMonitor);
            });
            Task.WaitAll(rectsPt1);       
            

            Thread.Sleep(1500);
            
            newRcvCfg.addMousePosition(pwhRect1);
            newRcvCfg.addMousePosition(pwhRect2);
            if (rcvConfig.ConfigIsDifferent(newRcvCfg))
            {
                DialogResult saveChanges = MessageBox.Show("Would you like to save these changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (saveChanges == DialogResult.Yes)
                {
                    //Debug.WriteLine(rcvConfig.getRectByType(pwhRect2.ControlType).getRectangle());

                    //Debug.WriteLine(newRcvCfg.getRectByType(pwhRect2.ControlType).getRectangle());
                    ConfigData cfgData = new ConfigData(ConfigData._userConfig, newRcvCfg, ConfigData._toolConfig);
                    cfgData.PrepareForSerialization();
                    ConfigData.SerializeConfigData();


                }

            }           
            pwhList.Clear();
            alteredRects.Clear();

            pwhList.Add(pwhRect3);
            pwhList.Add(pwhRect4);
            pwhList.Add(pwhRect5);

            DialogResult nextStepConfirm = MessageBox.Show("Now we are going to step two.\nPlease open any container's Receipt Lines window, once open, press ok to continue", "Next Step", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (nextStepConfirm != DialogResult.OK) { return; }
            Task rectsPt2 = Task.Run(() =>
            {
                alteredRects = RectanglesOverlay.Show(pwhList, PwhMonitor);
            });
            Task.WaitAll(rectsPt2);            
            newRcvCfg.addMousePosition(pwhRect3);
            newRcvCfg.addMousePosition(pwhRect4);
            newRcvCfg.addMousePosition(pwhRect5);
            if (ConfigData._receivingConfig.ConfigIsDifferent(newRcvCfg))
            {
                
                DialogResult saveChanges = MessageBox.Show("Would you like to save these changes?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                
                if (saveChanges == DialogResult.Yes)
                {
                    ConfigData cfgData = new ConfigData(ConfigData._userConfig, newRcvCfg, ConfigData._toolConfig);
                    cfgData.PrepareForSerialization();
                    ConfigData.SerializeConfigData();
                }
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

        internal static void receivingCleanUp(bool clearFailed = true)
        {
            endProcess = false;
            receivedItems.Clear();            
            receivedItems = new List<Item>();
            if (clearFailed)
            {
                failedItems.Clear();
                failedItems = new ConcurrentDictionary<int, Item>();
            }
            ReceivingGUI._instance.updateVisibility(ReceivingGUI._instance.overlayTip_lbl, false);
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

                if (rcvCfg.RectanglePositionList != null && rcvCfg.RectanglePositionList.Count > 0)
                {
                    foreach (var ctrlType in requiredCtrlTypes)
                    {
                        if (rcvCfg.getRectByType(ctrlType).getRectangle() == new Rectangle(0, 0, 150, 150))
                        {
                            return false;
                        }
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
    }
}
