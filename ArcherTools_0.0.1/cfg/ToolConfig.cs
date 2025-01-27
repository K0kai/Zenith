using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class ToolConfig
    {
        [XmlElement("EnableVoiceLine")]
        public bool EnableVoiceLines { get; set; }
        [XmlElement("AutoCreateConfig")]
        public bool AutomaticCreateConfig { get; set; }
        [XmlElement("CheckForDefault")]
        public bool CheckForDefault { get; set; }


        public ToolConfig() { }

        public ToolConfig(bool enableVoiceLines = false, bool automaticCreateCfg = false, bool checkForDefault = false)
        {
            EnableVoiceLines = enableVoiceLines;
            AutomaticCreateConfig = automaticCreateCfg;
            CheckForDefault = checkForDefault;

        }

        public bool ConfigIsDifferent(ToolConfig config)
        {
            if (config != null)
            {
                if (this.EnableVoiceLines != config.EnableVoiceLines || this.AutomaticCreateConfig != config.AutomaticCreateConfig || this.CheckForDefault != config.CheckForDefault)
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
