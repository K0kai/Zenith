using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.navigation;

namespace ArcherTools_0._0._1.methods
{

    internal class Inventory
    {

        public static int PwhMonitor;
        public static ReturnCodeEnum.ReturnCode SetInventoryCfg()
        {
            var processes = Process.GetProcessesByName("mstsc");
            if (processes.Length > 0)
            {
                WindowHandler.WinToFocusByName(processes[0].ProcessName);
            }
            else
            {
                MessageBox.Show("Please open the RDP first, then try again.", ReturnCodeEnum.ReturnCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ReturnCodeEnum.ReturnCode.WindowNotFound;
            }
            switch (WindowHandler.GetWindowPosition(processes[0].ProcessName).Value.X)
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

            ReceivingConfig rcvConfig = ConfigData._receivingConfig;

            if (rcvConfig == null)
            {
                MessageBox.Show("Receiving Config data is null. Possibly corrupted.", ReturnCodeEnum.ReturnCode.InvalidCfgData.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                rcvConfig = new ReceivingConfig();
            }
            ConfigData.setReceivingConfig(rcvConfig);

            return ReturnCodeEnum.ReturnCode.UnknownError;
        }
    }
}
