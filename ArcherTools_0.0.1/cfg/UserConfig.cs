using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class UserConfig
    {
        [XmlElement("UserCfgVersion")]
        public required string ConfigVersion {  get; set; }
        [XmlElement(IsNullable = true)]
        public string vpnUsername { get; set; }
        [XmlElement(IsNullable = true)]
        public string vpnPassword { get; set; }

        public UserConfig() { }
        public UserConfig(string vpnUsername, string vpnPassword)
        {
            this.vpnUsername = vpnUsername;
            this.vpnPassword = vpnPassword;
            this.ConfigVersion = ConfigData.ConfigVersion;
        }

        public static void setVpnUsername(string vpnUsername)
        {
            if (ConfigData._userConfig != null)
            {
                ConfigData._userConfig.vpnUsername = vpnUsername;
                ConfigData.SerializeConfigData();
            }
        }
        public static void setVpnPassword(string vpnPassword)
        {
            if (ConfigData._userConfig != null)
            {
                ConfigData._userConfig.vpnPassword = vpnPassword;
                ConfigData.SerializeConfigData();
            }
        }
    }
}
