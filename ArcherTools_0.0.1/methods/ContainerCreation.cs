using System.CodeDom;
using System.Collections.Concurrent;
using System.Diagnostics;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.navigation;
using InputSimulatorEx;
using NPOI.XWPF.UserModel;

namespace ArcherTools_0._0._1.methods
{
    internal class ContainerCreation
    {
        public static ConcurrentDictionary<string, int> ExcelValueColumns { get; set; } = new ConcurrentDictionary<string, int>();
        public static int MinimumRow { get; set; } = 1;

        internal static string FilePath;
        internal static string WorksheetName;

        internal static Point itemMtnIcon = Receiving.itemMtnIcon;
        internal static Point itemCfgIcon = Receiving.itemCfgIcon;
        internal static Point cloneItemIcon = new Point(516, 54);
        internal static Point itemSearchBox = Receiving.itemSearchBox;
        internal static Point receiptLnFirstLn = Receiving.receiptLnFirstLn;
        internal static Point receiptLineBorder = Receiving.receiptLineBorder;
        internal static Point ownerInputBox = Receiving.ownerInputBox;
        internal static Point itemMtnApparelButton = new Point(144, 244);

        internal static Point rlItemMtnIcon = new Point();
        internal static Point rlItemCfgIcon = new Point();
        internal static Point rlCloneItemIcon = new Point();
        internal static Point rlItemSearchBox = new Point();
        internal static Point rlReceiptLnFirstLn = new Point();
        internal static Point rlReceiptLnBorder = new Point();
        internal static Point rlOwnerInputBox = new Point();
        internal static Point rlItemMtnApparelButton = new Point();

        internal static int pwhMonitor;
        internal static int baseDelay = 500;
        internal static bool endProcess = false;
        internal static int iteration = 0;

        public static async Task<bool> CreateContainer(string owner)
        {
            var validConfig = Receiving.validateConfigData();
            var validRect = Receiving.validateRectanglePositions();
            var validExcel = Receiving.validateExcel();
            if (validConfig && validRect && validExcel)
            {
                if (!PowerHouseNavigation.Initialize())
                {
                    Debug.WriteLine("Failed to initialize the PowerHouse navigator");
                    return false;
                }
                endProcess = false;
                ReceivingConfig rcvCfg = ConfigData._receivingConfig;

                var processes = Process.GetProcessesByName("mstsc");
                if (processes.Length > 0)
                {
                    WindowHandler.WinToFocusByName(processes[0].ProcessName);
                }
                else
                {
                    MessageBox.Show("Please open the RDP first, then try again.", ReturnCodeEnum.ReturnCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                switch (WindowHandler.GetWindowPosition(processes[0].ProcessName).Value.X)
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

                Point rlItemMtnClose = new Point(pwhMonitor + rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Location.X + rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Width - 20, rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow).getRectangle().Location.Y + 15);
                Point rlItemCfgClose = new Point(pwhMonitor + rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Location.X + rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Width - 15, rcvCfg.getRectByType(ControlType.ItemConfigurationWindow).getRectangle().Location.Y + 15);


                rlReceiptLnFirstLn = toRelativePoint(rcvCfg.getRectByType(ControlType.ReceiptLineWindow), receiptLnFirstLn);
                rlReceiptLnBorder = toRelativePoint(rcvCfg.getRectByType(ControlType.ReceiptLineWindow), receiptLineBorder);
                rlItemMtnIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemMtnIcon);
                rlItemCfgIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemCfgIcon);
                rlOwnerInputBox = toRelativePoint(rcvCfg.getRectByType(ControlType.ItemSearchWindow), ownerInputBox);
                rlItemSearchBox = toRelativePoint(rcvCfg.getRectByType(ControlType.ItemSearchWindow), itemSearchBox);
                rlCloneItemIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), cloneItemIcon);
                var ItemMtnBoxRect = rcvCfg.getRectByType(ControlType.ItemMaintenanceWindow);
                rlItemMtnApparelButton = toRelativePoint(ItemMtnBoxRect, itemMtnApparelButton);
                Point rlLowerItemMtnBox = toRelativePoint(ItemMtnBoxRect, new Point(ItemMtnBoxRect.rect.Width / 2, (int)Math.Floor(ItemMtnBoxRect.rect.Height * 0.8)));
                ExcelHandler ExHandler = new ExcelHandler();
                try
                {
                    ExHandler = new ExcelHandler(FilePath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                var ips = new InputSimulator();

                MouseHandler.MouseMoveTo(rlOwnerInputBox);
                MouseHandler.MouseClick();

                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                ips.Keyboard.TextEntry(owner);
                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));

                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                MouseHandler.MouseMoveTo(rlItemSearchBox);
                MouseHandler.MouseClick();

                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.BACK);
                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);

                MouseHandler.MouseMoveTo(rlItemMtnIcon);
                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.4));
                MouseHandler.MouseClick();

                // TO BE LOOPED


                

                // TO BE LOOPED
                Task.Run(() =>
                {
                    checkForEnd();
                });
                if (endProcess) { return false; }
                
                var ExcelSize = ExHandler.GetColumn(WorksheetName, ExcelValueColumns["Item Code"], MinimumRow).Count + 1;
                Debug.WriteLine($"item code col: {ExcelValueColumns["Item Code"]}" + $"\nexcel name: {ExHandler.GetWorksheets()[0].Name}" + $"\nexcel file: {FilePath}");

                for (int x = 1; x <= ExcelSize; x++)
                {
                    if (endProcess) { return false; }
                    try
                    {
                        
                        var CurrentItem = GetItem(MinimumRow + x).Result;
                        if (string.IsNullOrEmpty(CurrentItem.itemCode))
                        {
                            continue;
                        }
                        Debug.WriteLine($"sum of:{MinimumRow + x}");
                        MouseHandler.MouseMoveTo(rlLowerItemMtnBox);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                        MouseHandler.MouseClick();
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));

                        MouseHandler.MouseMoveTo(rlItemMtnApparelButton);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                        MouseHandler.MouseClick();
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));

                        MouseHandler.MouseMoveTo(rlCloneItemIcon);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                        MouseHandler.MouseClick();
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 3.0));
                        Debug.WriteLine(CurrentItem.itemCode);
                        if (string.IsNullOrEmpty(CurrentItem.itemCode)) { return false; }
                        TabBetween(1);
                        ips.Keyboard.TextEntry(CurrentItem.itemCode);                        
                        TabBetween(1);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 2.25));
                        if (await CheckIfItemExists())
                        {
                            if (endProcess) { return false; }
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 3.0));
                            ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                            if (await CheckIfItemExists())
                            {
                                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                            }
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                            TabBetween(3);                            
                            ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);

                            PowerHouseNavigation.PWHMoveTo(PowerHouseNavigation.rlItemSearch_OwnerInputBox);
                            MouseHandler.MouseClick();

                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            ips.Keyboard.TextEntry(owner);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 2.5));
                            PowerHouseNavigation.PWHMoveTo(PowerHouseNavigation.rlItemSearchBox);
                            MouseHandler.MouseClick();
                            ips.Keyboard.TextEntry(CurrentItem.itemCode);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                            PowerHouseNavigation.PWHMoveTo(PowerHouseNavigation.rlItemMtnIcon);
                            MouseHandler.MouseClick();
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                            PowerHouseNavigation.PWHMoveTo(PowerHouseNavigation.rlItemCfgIcon);
                            MouseHandler.MouseClick();
                            if (endProcess) { return false; }
                            if (checkPCsForDefault())
                            {
                                if (endProcess) { return false; }
                                ips.Keyboard.TextEntry(CurrentItem.pieces + "PC");
                                TabBetween(3);
                                ips.Keyboard.TextEntry("1000");
                                TabBetween(2);
                                ips.Keyboard.TextEntry(CurrentItem.pieces.ToString());
                                if (x < 2)
                                {
                                    TabBetween(6);
                                    for (int i = 1; i <= 4; i++)
                                    {
                                        if (endProcess) { return false; }
                                        ips.Keyboard.TextEntry("0.0000");
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                        TabBetween(2);
                                    }
                                    TabBetween(2);
                                    for (int i = 1; i <= 4; i++)
                                    {
                                        if (endProcess) { return false; }
                                        ips.Keyboard.TextEntry("0.0000");
                                        Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                        TabBetween(2);
                                    }
                                }
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_S);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseMoveTo(rlItemCfgClose);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                                MouseHandler.MouseMoveTo(rlReceiptLnBorder);

                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                MouseHandler.MouseClick();
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 3.0));
                                ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.INSERT);
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                ips.Keyboard.TextEntry(CurrentItem.itemCode);
                                TabBetween(4);
                                ips.Keyboard.TextEntry(CurrentItem.total_pieces.ToString());
                                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_S);
                            }
                            continue;
                        }
                        ips.Keyboard.TextEntry(CurrentItem.description);                   
                        TabBetween(1);
                        ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));                        
                        ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.RETURN);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                        MouseHandler.MouseMoveTo(rlItemMtnApparelButton);
                        MouseHandler.MouseClick();
                        TabBetween(1);
                        ips.Keyboard.TextEntry(CurrentItem.style);
                        TabBetween(1);
                        ips.Keyboard.TextEntry(CurrentItem.color);
                        TabBetween(1);
                        ips.Keyboard.TextEntry(CurrentItem.size);
                        Thread.Sleep(baseDelay);
                        if (endProcess) { return false; }
                        ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_S);
                        Thread.Sleep((int)Math.Ceiling(baseDelay * 2.0));
                        MouseHandler.MouseMoveTo(rlItemCfgIcon);
                        MouseHandler.MouseClick();
                        Debug.WriteLine($"Initiating PC Check for: {x}");
                        if (checkPCsForDefault())
                        {
                            if (endProcess) { return false; }
                            ips.Keyboard.TextEntry(CurrentItem.pieces + "PC");
                            TabBetween(3);
                            ips.Keyboard.TextEntry("1000");
                            TabBetween(2);
                            ips.Keyboard.TextEntry(CurrentItem.pieces.ToString());
                            if (x < 2)
                            {
                                TabBetween(6);
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (endProcess) { return false; }
                                    ips.Keyboard.TextEntry("0.0000");
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                    TabBetween(2);
                                }
                                TabBetween(2);
                                for (int i = 1; i <= 4; i++)
                                {
                                    if (endProcess) { return false; }
                                    ips.Keyboard.TextEntry("0.0000");
                                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                                    TabBetween(2);
                                }
                            }
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_S);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            MouseHandler.MouseMoveTo(rlItemCfgClose);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            MouseHandler.MouseClick();
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 1.5));
                            MouseHandler.MouseMoveTo(rlReceiptLnBorder);
                            
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            MouseHandler.MouseClick();
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 3.0));
                            ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.INSERT);
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            ips.Keyboard.TextEntry(CurrentItem.itemCode);
                            TabBetween(4);
                            ips.Keyboard.TextEntry(CurrentItem.total_pieces.ToString());
                            Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                            ips.Keyboard.ModifiedKeyStroke(InputSimulatorEx.Native.VirtualKeyCode.CONTROL, InputSimulatorEx.Native.VirtualKeyCode.VK_S);



                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch(Exception ex)
                    {
                       // Debug.WriteLine(ex.Source);
                    }

                }

            }
            endProcess = true;
            return await Task.FromResult(true);
        } 
        
        private static bool checkPCsForDefault()
        {
            InputSimulator ips = new InputSimulator();
            Thread.Sleep((int)Math.Ceiling(baseDelay * 6.0));
            for (int i = 0; i < 5; i++)
            {
                Point mouseto = ScreenImageHandler.SearchImageOnScreen("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\defaultcfg.png", 0.99).Result;
                if (mouseto == new Point(0, 0))
                {
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.4));
                    ips.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.DOWN);
                    Thread.Sleep((int)Math.Ceiling(baseDelay * 0.5));
                    Debug.WriteLine("Nothing");
                }
                else
                {
                    Debug.WriteLine("Found the default config");
                    return true;
                }
            }
            return false;
        }

        private async static Task<bool> CheckIfItemExists()
        {
            Point mouseto = ScreenImageHandler.SearchImageOnScreen("C:\\Users\\Archer\\Source\\Repos\\Zenith\\ArcherTools_0.0.1\\img\\find\\errors\\item-exists.png", 0.90).Result;
            if (mouseto != new Point(0,0))
            {
                return true;
            }
            return false;
        }
        private async static Task<Item> GetItem(int row)
        {
            Dictionary<string, string> ItemParts = new Dictionary<string, string>();
            ExcelHandler ExHandler = new ExcelHandler(FilePath);
            foreach(var values in ExcelValueColumns)
            {
                var itemPart = ExHandler.GetCell(WorksheetName, row, values.Value);
                var nameOfItemPart = values.Key;
                if (nameOfItemPart == "Description" && string.IsNullOrEmpty(itemPart))
                {
                    itemPart = ExHandler.GetCell(WorksheetName, row, ExcelValueColumns["P.O"]);
                }
                
                ItemParts.TryAdd(nameOfItemPart, itemPart);
                Debug.WriteLine(nameOfItemPart + " : " + ItemParts[nameOfItemPart]);
                
            }
            Item FinalItem = new Item(ItemParts["Item Code"]);
            
            foreach (var itemPart in ItemParts)
            {
                if (string.IsNullOrEmpty(itemPart.Value))
                {
                    throw new MissingFieldException("Missing required item property."); 
                }
                switch (itemPart.Key)
                {
                    case "P.O":
                        try
                        {
                            FinalItem.po = int.Parse(itemPart.Value);
                        }
                        catch (Exception)
                        {
                            FinalItem.po = int.Parse(itemPart.Value.Split('/')[0]);
                        }
                        break;
                    case "Style":
                        FinalItem.style = itemPart.Value;
                        break;
                    case "Description":
                        FinalItem.description = itemPart.Value;
                        break;
                    case "Color":
                        FinalItem.color = itemPart.Value;
                        break;
                    case "Quantity":
                        FinalItem.pieces =  int.Parse(itemPart.Value);
                        break;
                    case "Total PC":
                        FinalItem.total_pieces = int.Parse(itemPart.Value);
                        break;
                    case "Size":
                        FinalItem.size = itemPart.Value;
                        break;

                }
            }
            return await Task.FromResult(FinalItem);
            
        }

        private static async Task<bool> checkForEnd()
        {
            InputSimulator ips = new InputSimulator();
            while (true)
            {

                if (ips.InputDeviceState.IsHardwareKeyDown(InputSimulatorEx.Native.VirtualKeyCode.END))
                {
                    Debug.WriteLine("pressed");
                    endProcess = true;
                    ips.Keyboard.KeyUp(InputSimulatorEx.Native.VirtualKeyCode.DELETE);
                    return true;
                }
                Thread.Sleep(25);

            }

        }

        private static void TabBetween(int times, int start = 1)
        {
            var inputSim = new InputSimulator();
            for (int i = start; i <= times; i++)
            {
                inputSim.Keyboard.KeyPress(InputSimulatorEx.Native.VirtualKeyCode.TAB);
                Thread.Sleep((int)Math.Ceiling(baseDelay * 0.35));
            }
        }

        private static Point toRelativePoint(PowerHouseRectangles rect, Point relative)
        {
            if (!Receiving.validateConfigData() && Receiving.validateRectanglePositions())
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
    }
}
