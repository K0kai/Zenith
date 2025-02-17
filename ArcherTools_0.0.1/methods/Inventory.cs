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

namespace ArcherTools_0._0._1.methods
{

    internal class Inventory
    {

        public static int PwhMonitor;
        public static ErrorEnum.ErrorCode SetInventoryCfg()
        {
            var processes = Process.GetProcessesByName("mstsc");
            if (processes.Length > 0)
            {
                WindowHandler.WinToFocusByName(processes[0].ProcessName);
            }
            else
            {
                MessageBox.Show("Please open the RDP first, then try again.", ErrorEnum.ErrorCode.WindowNotFound.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ErrorEnum.ErrorCode.WindowNotFound;
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
                MessageBox.Show("Receiving Config data is null. Possibly corrupted.", ErrorEnum.ErrorCode.InvalidCfgData.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                rcvConfig = new ReceivingConfig();
            }
            ConfigData.setReceivingConfig(rcvConfig);

            return ErrorEnum.ErrorCode.UnknownError;
        }
    }
}
