using System.Collections.Concurrent;
using System.Diagnostics;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.enums;
using InputSimulatorEx;

namespace ArcherTools_0._0._1.methods
{
    internal class ContainerCreation
    {
        public static ConcurrentDictionary<string, int> ExcelValueColumns { get; set; } = new ConcurrentDictionary<string, int>();
        public static int MinimumRow { get; set; } = 1;

        internal static Point itemMtnIcon = new Point(355, 54);
        internal static Point itemCfgIcon = new Point(264, 54);
        internal static Point receiptLnFirstLn = new Point(55, 52);

        internal static Point rlItemMtnIcon = new Point();
        internal static Point rlItemCfgIcon = new Point();
        internal static Point rlReceiptLnFirstLn = new Point();

        internal static int pwhMonitor;
        internal static int baseDelay = 500;
        internal static bool endProcess = false;
        internal static int iteration = 0;

        public async Task<bool> CreateContainer()
        {
            var validConfig = Receiving.validateConfigData();
            var validRect = Receiving.validateRectanglePositions();
            var validExcel = Receiving.validateExcel();
            if (validConfig && validRect && validExcel)
            {
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
                rlItemMtnIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemMtnIcon);
                rlItemCfgIcon = toRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemCfgIcon);
            }
            return await Task.FromResult(true);
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
                    return true;
                }
                Thread.Sleep(25);

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
