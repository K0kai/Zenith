
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class ConfigData : ConfigDataBase
    {
        private const string CurrentVersion = "2.0";


<<<<<<< Updated upstream
        public MousePosition() { }

        public MousePosition(ControlType controlType, List<int> coordinates)
        {
            ControlType = controlType;
            Coordinates = coordinates;
        }

        public List<int> getPositionByType(ControlType type)
        {
            if (type == ControlType)
            {
                return Coordinates;
            }
            else
            {
                return new List<int> { 0, 0 };
            }
        }
    }

    [Serializable]
    public class UserConfig
    {
        public required string vpnUsername { get; set; }
        public required string vpnPassword { get; set; }

        public UserConfig(string vpnUsername, string vpnPassword)
        {
            this.vpnUsername = vpnUsername;
            this.vpnPassword = vpnPassword;
        }
    }


    [Serializable]
    public class ReceivingConfig
    {
        public required string ExcelFilePath { get; set; }
        public required string ExcelSheetName { get; set; }

        [XmlArray("MousePositionList")]
        [XmlArrayItem("MousePosition")]
        public List<MousePosition> MousePositionList { get; set; } = new List<MousePosition>();

        public ReceivingConfig() { }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
        public ReceivingConfig(string excelFilePath, string excelSheetName, List<MousePosition> mousePositionList)
        {
            ExcelFilePath = excelFilePath;
            ExcelSheetName = excelSheetName;
            MousePositionList = mousePositionList;
        }

        public List<MousePosition> getMousePositions() { return MousePositionList; }
        public string getExcelFilePath() { return ExcelFilePath; }

        public void setExcelFilePath(string filePath) { ExcelFilePath = filePath; }

        public void setMousePositions(List<MousePosition> mousePositions) { MousePositionList = mousePositions; }

        public void addMousePosition(MousePosition valueToAdd) { MousePositionList.Add(valueToAdd); }
=======
        //Non serializable variables
        public static string ConfigVersion { get; set; } = CurrentVersion;
        public static UserConfig? _userConfig { get; private set; }
        
        public static ReceivingConfig? _receivingConfig { get; private set; }
        
>>>>>>> Stashed changes

        //Serializable variables
        [XmlElement("XmlCfgVersion")]
        public string _serializableCfgVersion;
        [XmlElement("userConfiguration")]
        public UserConfig? _serializableUserConfig;
        [XmlElement("receivingConfiguration")]
        public ReceivingConfig? _serializableRcvConfig;
        

<<<<<<< Updated upstream
    public class ConfigData
    {
        public static ReceivingConfig _receivingConfig;
        public static UserConfig _userConfig;

        public ConfigData(ReceivingConfig receivingConfig, UserConfig userConfig = null)
        {
            _receivingConfig = receivingConfig;
            _userConfig = userConfig;
        }

        public ConfigData() { }

        public static void SerializeConfig()
        {
=======
        public ConfigData() { }            
        public ConfigData(UserConfig userConfig, ReceivingConfig receivingConfig)
        {
            ConfigVersion = CurrentVersion;
            _userConfig = userConfig;
            _receivingConfig = receivingConfig;            
            _serializableCfgVersion = ConfigVersion;
            _serializableUserConfig = userConfig;
            _serializableRcvConfig = receivingConfig;
            
        }

        public void PrepareForSerialization()
        {
            _serializableCfgVersion = ConfigVersion;
            _serializableUserConfig = _userConfig;    
            _serializableRcvConfig= _receivingConfig;
        }

        public void PostDeserialization()
        {
            ConfigVersion = _serializableCfgVersion;
            _userConfig = _serializableUserConfig;
            _receivingConfig= _serializableRcvConfig;
            
        }

        public static void SerializeConfigData()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);
            
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
                serializer.Serialize(fileStream, new ConfigData(_userConfig, _receivingConfig));
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
>>>>>>> Stashed changes
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
<<<<<<< Updated upstream
                if (Directory.Exists(directoryPath))
                {
                    Process.Start("explorer.exe", directoryPath);
=======
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Config data file does not exist.");
                }
                var serializer = new XmlSerializer(typeof(ConfigData));
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return (ConfigData?)serializer.Deserialize(fileStream);
>>>>>>> Stashed changes
                }
            }
            catch (Exception ex)
            {
<<<<<<< Updated upstream
                Console.WriteLine($"Failed to open directory: {ex.Message}");
=======
                Debug.WriteLine(ex.Message);
                return null;
>>>>>>> Stashed changes
            }

            // Serialize the entire ConfigData class
            var serializer = new XmlSerializer(typeof(ConfigData));
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, new ConfigData(_receivingConfig, _userConfig));
                Console.WriteLine($"Config file {(File.Exists(filePath) ? "updated" : "created")} at {filePath}");
            }
        }

        public static ReceivingConfig getRcvCfgInstance() { return _receivingConfig; }
        public static UserConfig getUserCfgInstance() { return _userConfig; }

        public static ConfigData UnserializeConfig()
        {
            // File setup
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Config file not found.");
                return null;
            }

            // Deserialize the ConfigData class
            var serializer = new XmlSerializer(typeof(ConfigData));
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (ConfigData)serializer.Deserialize(fileStream);
            }
        }

        public static void setReceivingConfig(ReceivingConfig receivingConfig)
        {
            _receivingConfig = receivingConfig;
        }

        public string GetConfigDetails()
        {
            return ConfigVersion;
        }
    }
          
}
