using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1.navigation
{
    class PowerHouseNavigation
    {
        internal static int PwhMonitor = 0;
        //RECEIPT LINES
        internal static Point receiptLineBorder = new Point(18, 33);
        internal static Point receiptLnFirstLn = new Point(55, 52);
        internal static Point rlReceiptLineBorder = new Point();
        internal static Point rlReceiptLnFirstLn = new Point();
        //RECEIPT LINES

        //ICONS
        internal static Point cloneItemIcon = new Point(516, 54);
        internal static Point itemMtnIcon = new Point(355, 54);
        internal static Point itemCfgIcon = new Point(264, 54);
        internal static Point rlItemMtnIcon = new Point();
        internal static Point rlItemCfgIcon = new Point();
        internal static Point rlCloneItemIcon = new Point();
        //ICONS

        //ITEM CONFIGURATION
        internal static Point itemCfgPcsBox = new Point(160, 38);
        internal static Point rlItemCfgPcsBox = new Point();
        //ITEM CONFIGURATION

        //ITEM SEARCH
        internal static Point itemSearchBox = new Point(190, 90);
        internal static Point ownerInputBox = new Point(160, 70);
        internal static Point rlItemSearchBox = new Point();
        internal static Point rlOwnerInputBox = new Point();
        //ITEM SEARCH

        //ITEM MAINTENANCE
        internal static Point itemMtnApparelButton = new Point(144, 244);
        internal static Point rlItemMtnApparelButton = new Point();
        //ITEM MAINTENANCE

        public static void DetectPwHMonitor()
        {
            var processes = Process.GetProcessesByName("mstsc");
            if (processes.Length > 0)
            {
                WindowHandler.WinToFocusByName(processes[0].ProcessName);
            }
            else
            {
                MessageBox.Show("Please open the RDP first, then try again.", ReturnCodeEnum.ReturnCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            switch (WindowHandler.GetWindowPosition(processes[0].ProcessName).Value.X)
            {
                case >= 3840:
                    PwhMonitor = 3840;
                    break;
                case >= 1920:
                    PwhMonitor = 1920;
                    break;
                default:
                    PwhMonitor = 0;
                    break;
            }
        }
        internal static void SetUpRelativePositions()
        {
            var validConfig = Receiving.validateConfigData();
            var validRect = Receiving.validateRectanglePositions();
            var validExcel = Receiving.validateExcel();
            if (validConfig && validRect && validExcel)
            {
                var rcvCfg = ConfigData._receivingConfig;
                rlReceiptLineBorder = ToRelativePoint(rcvCfg.getRectByType(ControlType.ReceiptLineWindow), receiptLineBorder);
                rlReceiptLnFirstLn = ToRelativePoint(rcvCfg.getRectByType(ControlType.ReceiptLineWindow), receiptLnFirstLn);
                rlItemSearchBox = ToRelativePoint(rcvCfg.getRectByType(ControlType.ItemSearchWindow), itemSearchBox);
                rlOwnerInputBox = ToRelativePoint(rcvCfg.getRectByType(ControlType.ItemSearchWindow), ownerInputBox);
                rlItemMtnIcon = ToRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemMtnIcon);
                rlItemCfgIcon = ToRelativePoint(rcvCfg.getRectByType(ControlType.PowerHouseUpperTab), itemCfgIcon);
                rlItemCfgPcsBox = ToRelativePoint(rcvCfg.getRectByType(ControlType.ItemConfigurationWindow), itemCfgPcsBox);
            }
        }
        private static Point ToRelativePoint(PowerHouseRectangles rect, Point relative)
        {
            DetectPwHMonitor();
            if (!Receiving.validateConfigData() && Receiving.validateRectanglePositions())
            {
                throw new InvalidDataException("Incorrect config set up for this operation.");
            }
            ReceivingConfig rcvCfg = ConfigData._receivingConfig;

            Point relativePoint = new Point();
            Rectangle convRect = rect.getRectangle();
            int X = PwhMonitor + convRect.Location.X + relative.X;
            int Y = convRect.Location.Y + relative.Y;
            relativePoint = new Point(X, Y);
            return relativePoint;

        }












    }
}
