using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    public enum ControlType
    {
        ReceiptLineLeftSide,
        ReceiptLineFirstLine,
        ItemSearchInputBox,
        ItemMaintenanceBox,
        ItemConfigurationBox,
        NumPiecesInputBox,
        PalletWeightInputBox,
        PalletHeightInputBox,
        PalletWidthInputBox,
        PalletDepthInputBox,
        CasesWeightInputBox,
        CasesHeightInputBox,
        CasesWidthInputBox,
        CasesDepthInputBox,
        CasesPerPalletInputBox,
        CasesPerTierInputBox
    }

    [Serializable]
    public class MousePosition
    {
        [XmlElement("ControlType")]
        public ControlType ControlType { get; set; }

        [XmlArray("Coordinates")]
        [XmlArrayItem("Coordinate")]
        public List<int> Coordinates { get; set; } = new List<int>();

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

    }

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
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Process.Start("explorer.exe", directoryPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open directory: {ex.Message}");
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
    }
}