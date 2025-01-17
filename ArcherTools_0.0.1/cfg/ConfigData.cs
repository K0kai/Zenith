
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class ConfigData : ConfigDataBase
    {
        private const string CurrentVersion = "2.2";


        //Non serializable variables
        public static string  ConfigVersion { get; set; } = CurrentVersion;
        public static UserConfig? _userConfig { get; private set; }
        public static ReceivingConfig? _receivingConfig { get; private set; }
        public static ToolConfig? _toolConfig { get; private set; }
        public static string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string fileName = "Config.xml";
        public static string filePath = Path.Combine(directoryPath, fileName);


        //Serializable variables
        [XmlElement("XmlCfgVersion")]
        public string _serializableCfgVersion;
        [XmlElement("userConfiguration")]
        public UserConfig? _serializableUserConfig;
        [XmlElement("receivingConfiguration")]
        public ReceivingConfig? _serializableRcvConfig;
        [XmlElement("toolConfiguration")]
        public ToolConfig? _serializableToolConfig;


        public ConfigData() { }
        public ConfigData(UserConfig userConfig, ReceivingConfig receivingConfig, ToolConfig toolConfig)
        {
            ConfigVersion = CurrentVersion;
            _userConfig = userConfig;
            _receivingConfig = receivingConfig;
            _toolConfig = toolConfig;
            _serializableCfgVersion = ConfigVersion;
            _serializableUserConfig = userConfig;
            _serializableRcvConfig = receivingConfig;
            _serializableToolConfig = toolConfig;
        }

        public void PrepareForSerialization()
        {
            _serializableCfgVersion = ConfigVersion;
            _serializableUserConfig = _userConfig;
            _serializableRcvConfig = _receivingConfig;
            _serializableToolConfig = _toolConfig;
        }

        public void PostDeserialization()
        {
            ConfigVersion = _serializableCfgVersion;
            _userConfig = _serializableUserConfig;
            _receivingConfig = _serializableRcvConfig;
            _toolConfig = _serializableToolConfig;

        }

        public static void SerializeConfigData()
        {          
            try
            {
                if (Directory.Exists(directoryPath))
                {

                }
                else
                {
                    throw new DirectoryNotFoundException($"{directoryPath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            var serializer = new XmlSerializer(typeof(ConfigData));
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, new ConfigData(_userConfig, _receivingConfig, _toolConfig));
                Debug.WriteLine($"Serialized config file at path \"{filePath}\".");
#if DEBUG
                if (Directory.Exists(directoryPath))
                {
                    Process.Start("explorer.exe", filePath);
                }
#endif
            }


        }

        public static ConfigData? DeserializeConfigData()
        {        
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Config data file does not exist at path {filePath}.");
                }
                var serializer = new XmlSerializer(typeof(ConfigData));
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    if (new FileInfo(filePath).Length == 0)
                    {
                        return null;
                    }
                    return (ConfigData?)serializer.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var _ = createNewCfgFile();
                if (_ == true)
                {
                    var cfgData = new ConfigData(new UserConfig(), new ReceivingConfig(ConfigVersion), new ToolConfig());
                    cfgData.PrepareForSerialization();
                    SerializeConfigData();
                    return DeserializeConfigData();                    
                }
                return null;
            }

        }

        public static bool createNewCfgFile()
        {
            try
            {
                using (File.Create(filePath))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public static void setReceivingConfig(ReceivingConfig receivingConfig)
        {
            _receivingConfig = receivingConfig;
        }

        public static void setToolConfig(ToolConfig toolConfig)
        {
            _toolConfig = toolConfig;
        }

        public static void setUserConfig(UserConfig userConfig)
        {
            _userConfig = userConfig;
        }

        public string GetConfigDetails()
        {
            return ConfigVersion;
        }
    }

}