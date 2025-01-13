using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class ToolConfig
    {
        public bool EnableVoiceLines { get; set; }

        public byte PowerHouseMonitor {  get; set; }

        public ToolConfig() { }

        public ToolConfig(bool enableVoiceLines = false, byte PWhMonitor = 1)
        {
            EnableVoiceLines = enableVoiceLines;
            PowerHouseMonitor = PWhMonitor;

        }
    }
}
