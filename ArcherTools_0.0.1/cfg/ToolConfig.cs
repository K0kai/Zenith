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


        public ToolConfig() { }

        public ToolConfig(bool enableVoiceLines = false)
        {
            EnableVoiceLines = enableVoiceLines;

        }

        public bool ConfigIsDifferent(ToolConfig config)
        {
            if (config != null)
            {
                if (this.EnableVoiceLines != config.EnableVoiceLines)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;

        }
    }
}
